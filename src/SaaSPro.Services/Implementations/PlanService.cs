using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SaaSPro.Common;
using SaaSPro.Common.Helpers;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Infrastructure.Payment;
using SaaSPro.Services.Helpers;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Mapping;
using SaaSPro.Services.Messaging.PlanService;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Common;
using Stripe;

namespace SaaSPro.Services.Implementations
{
	public class PlanService : IPlanService
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IEmailTemplatesRepository _emailTemplatesRepository;
		private readonly IPlanInfoRepository _planInfoRepository;
		private readonly IPlanInfoValueRepository _planInfoValueRepository;
		private readonly IPlanRepository _planRepository;
		private readonly IReferenceListRepository _referenceListRepository;
		private readonly string[] _reservedSubdomains = {"admin", "demo", "www"};
		private readonly IRoleRepository _roleRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepository _userRepository;

		public PlanService(IUnitOfWork unitOfWork, IPlanRepository planRepository,
			IPlanInfoRepository planInfoRepository, ICustomerRepository customerRepository,
			IPlanInfoValueRepository planInfoValueRepository, IRoleRepository roleRepository,
			IUserRepository userRepository, IReferenceListRepository referenceListRepository,
			IEmailTemplatesRepository emailTemplatesRepository)
		{
			_unitOfWork = unitOfWork;
			_planRepository = planRepository;
			_planInfoRepository = planInfoRepository;
			_customerRepository = customerRepository;
			_planInfoValueRepository = planInfoValueRepository;
			_roleRepository = roleRepository;
			_userRepository = userRepository;
			_referenceListRepository = referenceListRepository;
			_emailTemplatesRepository = emailTemplatesRepository;
		}

		public List<Plan> List()
		{
			return _planRepository.Query().ToList();
		}

		public PlansListModel List(PagingCommand command)
		{
			var plans = _planRepository.FetchPaged(q => q.OrderBy(t => t.OrderIndex), command.PageIndex,
				command.PageSize);
			var model = new PlansListModel
			{
				Plans = Mapper.Engine.MapPaged<Plan, PlansListModel.PlanSummary>(plans)
			};
			return model;
		}

		public void Add(PlanAddModel model)
		{
			var plan = new Plan(model.Name, model.Price, model.Period, model.OrderIndex, model.PlanCode, true);
			_planRepository.Add(plan);
			_unitOfWork.Commit();
		}

		public Plan Get(Guid id)
		{
			return _planRepository.Get(id);
		}

		public void Update(Plan plan)
		{
			plan.UpdatedOn = DateTime.UtcNow;

			_planRepository.Update(plan);
			_unitOfWork.Commit();
		}

		public void Delete(Plan plan)
		{
			_planRepository.Delete(plan);
			_unitOfWork.Commit();
		}

		public PricingModel GetPricing()
		{
			var pricingModel = new PricingModel
			{
				Plans = _planRepository.Query().OrderBy(p => p.OrderIndex).ToList(),
				PlanInfoItems = _planInfoRepository.Query().OrderBy(i => i.OrderIndex).ToList(),
				PlanInfoValues = _planInfoValueRepository.Query().ToList()
			};
			return pricingModel;
		}

		public bool PlanCodeExist(string plan)
		{
			var planCodeExist = _planRepository.Query().Any(p => p.PlanCode.ToLower() == plan.ToLower());
			return planCodeExist;
		}

		public bool IsHostNameAvailable(string hostName)
		{
			if (string.IsNullOrWhiteSpace(hostName) || hostName.Contains(".") || _reservedSubdomains.Contains(hostName.ToLower()))
				return false;
			return !_customerRepository.Query().Any(t => t.Hostname.ToLower() == hostName.ToLower());
		}

		/// <summary>
		/// Sign up a user to a plan
		/// </summary>
		/// <param name="request">Sign up request</param>
		public PlanSignUpResponse PlanSignUp(PlanSignUpRequest request)
		{
			try
			{
				// Begin transaction
				_unitOfWork.Begin("PlanSignUp");

				var response = new PlanSignUpResponse();
				var dbPlan = _planRepository.Query().FirstOrDefault(p => p.PlanCode.ToLower() == request.PlanName.ToLower());
				if (dbPlan == null)
				{
					response.HasError = true;
					response.ErrorCode = ErrorCode.PlanNotFound;
					return response;
				}

				if (request.SignUpModel.Domain != null)
				{
					if (
						_customerRepository.Query().Any(
							t => t.Hostname.ToLower() == request.SignUpModel.Domain.ToLower()))
					{
						response.ErrorCode = ErrorCode.DomainAlreadyExists;
						response.HasError = true;
						return response;
					}
				}

				// Customer
				var customer = new Customer(request.SignUpModel.FullName, request.SignUpModel.Domain,
					request.SignUpModel.Company);
				_customerRepository.Add(customer);

				// Role
				var role = new Role(customer, "Administrator", true, UserType.SystemUser);
				_roleRepository.Add(role);

				// Setup the User
				var user = new User(customer, request.SignUpModel.Email, request.SignUpModel.FirstName,
					request.SignUpModel.LastName, request.SignUpModel.Password);
				_userRepository.Add(user);

				role.AddUser(user);

				customer.UpdateAdminUser(user);

				// Security questions
				var referenceList = _referenceListRepository.Query().FirstOrDefault(l => l.SystemName == "Security Questions");
				if (referenceList == null)
				{
					throw new NullReferenceException("Security questions reference list is null");
				}

				foreach (var securityQuestion in SecurityQuestions.Questions)
				{
					referenceList.AddItem(customer, securityQuestion);
				}

				// User security questions
				user.AddSecurityQuestion("a", "a");
				user.AddSecurityQuestion("b", "b");
				user.AddSecurityQuestion("c", "c");

				// Create customer in stripe
				var stripeCustomer = StripeFactory.GetStripeService().CreateCustomer(request.SignUpModel.FullName,
					request.SignUpModel.Email);
				customer.PaymentCustomerId = stripeCustomer.Id;

				// Associate plan
				var result = StripeFactory.GetStripeService().AssignCustomerPlan(customer.PaymentCustomerId,
					dbPlan.PlanCode,
					request.SignUpModel.CardNumber,
					request.SignUpModel.SecurityCode,
					int.Parse(
						request.SignUpModel.Expiration.
							Substring(0, 2)),
					int.Parse(
						request.SignUpModel.Expiration.
							Substring(3, 2)));

				if (result != "active")
				{
					throw new Exception($"Incorrect assigning plan result for Customer {customer.FullName}");
				}

				customer.UpdatePlan(dbPlan);
				_customerRepository.Update(customer);

				// End transaction
				_unitOfWork.End();

				// Send email
				var emailTemplate =
					_emailTemplatesRepository.GetTemplateByName(
						EmailTemplateCode.SignUpGreeting.ToDescriptionString());

				emailTemplate = EmailTemplateFactory.ParseTemplate(emailTemplate, customer);
				var mailMessage = MailMessageMapper.ConvertToMailMessage(emailTemplate);
				var emailResult = EmailServiceFactory.GetEmailService().SendMail(mailMessage);

				if (!emailResult)
				{
					throw new Exception("Problem sending email");
				}

				return response;
			}
			catch (StripeException ex)
			{
				LoggingFactory.GetLogger().LogError(ex);
				throw new Exception("Assigning customer plan error", ex);
			}
			catch (Exception ex)
			{
				LoggingFactory.GetLogger().LogError(ex);
				throw;
			}
		}
	}
}