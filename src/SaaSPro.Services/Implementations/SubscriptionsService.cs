using System;
using System.Linq;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Infrastructure.Payment;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.SubscriptionsService;
using SaaSPro.Services.ViewModels;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using SaaSPro.Common;
using Stripe;


namespace SaaSPro.Services.Implementations
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPlanRepository _planRepository;
      

        public SubscriptionsService(IUnitOfWork unitOfWork,ICustomerRepository customerRepository,IPlanRepository planRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _planRepository = planRepository;
        
        }

        public GetSubscriptionsResponse List(Guid customerId)
        {
            var response = new GetSubscriptionsResponse();   

            var customer = _customerRepository.Get(customerId);

            if (customer.Plan == null)
            {
                response.HasPlan = false;
                response.ChangeSubscriptionModel.Plans = _planRepository.Query().ToList();
                return response;
            }
           
                response.HasPlan = true;
            

            response.SubscriptionModel = new SubscriptionModel()
                                             {
                                                 CurrentPlan = customer.Plan != null ? customer.Plan.Name : string.Empty,
                                                 CurrentPeriod = customer.Plan != null ? customer.Plan.Period : string.Empty,
                                                 CurrentPrice = customer.Plan != null ? customer.Plan.Price : 0.0M
                                             };
            return response;
        }


        public CancelSubscriptionResponse CancelSubscription(Guid customerId)
        {
            var response = new CancelSubscriptionResponse();

            var customer = _customerRepository.Get(customerId);

            if (customer.Plan != null)
            {
                var status = StripeFactory.GetStripeService().CloseCustomerPlan(customer.PaymentCustomerId);

                if (status.ToLower() == "active")
                {
                    response.IsStatusActive = true;
                    return response;
                }
                customer.RemovePlan();
                _customerRepository.Update(customer);

                _unitOfWork.Commit();
            }
            return response;
        }

        public ChangeSubscriptionResponse ChangeSubscription(ChangeSubscriptionRequest request)
        {
            var response = new ChangeSubscriptionResponse();

            try
            {
                Customer customer = _customerRepository.Get(request.CustomerId);

                if (customer.Plan != null)
                {
                    var status = StripeFactory.GetStripeService().CloseCustomerPlan(customer.PaymentCustomerId);
                    if (status.ToLower() == "active")
                    {
                        response.IsStatusActive = true;
                        return response;
                    }
                }

                Plan plan =
                    _planRepository.Query().SingleOrDefault(p => p.PlanCode.ToLower() == request.PlanId.ToLower());

                if (plan == null)
                {
                    response.ErrorCode = ErrorCode.IncorrectPlanIdentifier;
                    return response;
                }

                string result = StripeFactory.GetStripeService().AssignCustomerPlan(customer.PaymentCustomerId,
                                                                  request.ChangeSubscriptionModel.PlanId,
                                                                  request.ChangeSubscriptionModel.CardNumber,
                                                                  request.ChangeSubscriptionModel.SecurityCode,
                                                                  request.ChangeSubscriptionModel.ExpirationMonth,
                                                                  request.ChangeSubscriptionModel.ExpirationYear);


                if (result != "active")
                {
                    response.Result = result;
                    return response;
                }

                customer.UpdatePlan(plan);

                _unitOfWork.Commit();
            }
            catch (StripeException ex)
            {
                LoggingFactory.GetLogger().LogError(ex);
                response.HasError = true;
                response.ErrorCode = ErrorCode.StripeSetupError;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                LoggingFactory.GetLogger().LogError(ex);
                response.HasError = true;
                response.ErrorCode = ErrorCode.StripeSetupError;
                response.Message = ErrorCode.StripeSetupError.ToString();
            }


            return response;
        }
    }
}
