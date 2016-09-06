using System;
using System.Linq;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Infrastructure.Payment;
using SaaSPro.Services.Helpers;
using SaaSPro.Services.Interfaces;
using SaaSPro.Common;
using Stripe;
using System.Net.Mail;
using SaaSPro.Services.Mapping;
using Newtonsoft.Json.Linq;
using SaaSPro.Resources;

namespace SaaSPro.Services.Implementations
{
    public class StripeWebhookService : IStripeWebhookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerPaymentRefundRepository _customerPaymentRefundRepository;
        private readonly ICustomerPaymentRepository _customerPaymentRepository;
        private readonly IEmailTemplatesRepository _emailTemplatesRepository;


        public StripeWebhookService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository,
                                    ICustomerPaymentRefundRepository customerPaymentRefundRepository,
                                    ICustomerPaymentRepository customerPaymentRepository,
                                    IEmailTemplatesRepository emailTemplatesRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _customerPaymentRefundRepository = customerPaymentRefundRepository;
            _customerPaymentRepository = customerPaymentRepository;
            _emailTemplatesRepository = emailTemplatesRepository;
        }

        /// <summary>
        /// Process the incoming Stripe Event
        /// </summary>
        public void ProcessEvent(string customerId, StripeEvent stripeEvent)
        {
            try
            {
                LoggingFactory.GetLogger().Log($"Start ProcessEvent(EventType={stripeEvent.Type})", EventLogSeverity.Debug);

                EmailTemplateCode emailTemplateCode = EmailTemplateCode.None;

                Customer customer = _customerRepository.Query().FirstOrDefault(p => p.PaymentCustomerId == customerId);
                if (customer == null)
                {
                    LoggingFactory.GetLogger().Log(Messages.NotFindCustomerByPaymentCustomerId, EventLogSeverity.Error);
                    throw new Exception(Messages.NotFindCustomerByPaymentCustomerId + customerId);
                }

                StripeSubscription subscription;

                switch (stripeEvent.Type)
                {
                    case "charge.succeeded":
                        var charge = Mapper<StripeCharge>.MapFromJson(stripeEvent.Data.Object.ToString());
                        var payment = new CustomerPayment(customer.Id, charge.BalanceTransactionId, false, Convert.ToDecimal(charge.Amount / 100.0), charge.Created);

                        _customerPaymentRepository.Add(payment);

                        LoggingFactory.GetLogger().Log("Customer payment added", EventLogSeverity.Information);

                        emailTemplateCode = EmailTemplateCode.ChargeSuccessfull;

                        break;
                    case "charge.refunded":
                        var refundedCharge = Mapper<StripeCharge>.MapFromJson(stripeEvent.Data.Object.ToString());
                        var stripeEventObject = JObject.Parse(stripeEvent.Data.Object.ToString());
                        var refundsData = stripeEventObject.SelectToken("refunds.data");
                        StripeApplicationFeeRefund[] refunds = Mapper<StripeApplicationFeeRefund[]>.MapFromJson(refundsData.ToString());

                        StripeApplicationFeeRefund lastRefund = refunds[refunds.Length - 1];
                        var amountRefundedInDollars = (decimal) (lastRefund.Amount / 100.0);

                        var refundPayment = new CustomerPaymentRefund(customer.Id, refundedCharge.BalanceTransactionId,
                                                                    refundedCharge.Id,
                                                                    amountRefundedInDollars, lastRefund.Created);

                        _customerPaymentRefundRepository.Add(refundPayment);

                        // TODO update refund field in payment record
                        //refundedCharge.Refunded
                        LoggingFactory.GetLogger().Log("Customer payment refunding", EventLogSeverity.Information);

                        emailTemplateCode = EmailTemplateCode.ChargeRefunded;

                        break;
                    case "charge.failed":

                        // Update the DB

                        emailTemplateCode = EmailTemplateCode.ChargeFailed;
                        LoggingFactory.GetLogger().Log("Customer payment failed", EventLogSeverity.Information);

                        //TODO:

                        break;
                    case "customer.subscription.deleted":

                        // look up user details from the Customer
                        subscription = Mapper<StripeSubscription>.MapFromJson(stripeEvent.ToString());
                        customer.PlanCanceledOn = subscription.CanceledAt ?? DateTime.Now;
                        _customerRepository.Update(customer);

                        //TODO:
                        //- Create log entry for Admins
                        emailTemplateCode = EmailTemplateCode.CustomerSubscriptionDeleted;
                        LoggingFactory.GetLogger().Log("Customer subscription deleted", EventLogSeverity.Information);

                        break;
                    case "customer.subscription.updated":

                        // look up user details from the Customer
                        subscription = Mapper<StripeSubscription>.MapFromJson(stripeEvent.ToString());
                        customer.PlanUpdatedOn = subscription.Start ?? DateTime.Now;
                        _customerRepository.Update(customer);

                        //TODO:
                        //- Create log entry for Admins
                        emailTemplateCode = EmailTemplateCode.CustomerSubscriptionUpdated;
                        LoggingFactory.GetLogger().Log("Customer subsciption updated", EventLogSeverity.Information);

                        break;
                    case "charge.created":
                        //TODO 
                        return;
                }


                if (emailTemplateCode != EmailTemplateCode.None)
                {

                    EmailTemplate emailTemplate =
                        _emailTemplatesRepository.GetTemplateByName(
                            Common.Helpers.Utilities.ToDescriptionString(emailTemplateCode));

                    emailTemplate = EmailTemplateFactory.ParseTemplate(emailTemplate, customer);
                    MailMessage mailMessage = MailMessageMapper.ConvertToMailMessage(emailTemplate);
                    bool result = EmailServiceFactory.GetEmailService().SendMail(mailMessage);

                    if (result)
                    {
                        _unitOfWork.Commit();
                        LoggingFactory.GetLogger().Log("Payment information saved", EventLogSeverity.Information);
                    }
                    else
                    {
                        LoggingFactory.GetLogger().Log("There is an error creating Customer payment", EventLogSeverity.Fatal);
                        throw new Exception("There is an error creating Customer payment");
                    }

                }

                LoggingFactory.GetLogger().Log("End ProcessEvent({0})", EventLogSeverity.Debug);
            }
            catch (Exception ex)
            {
                LoggingFactory.GetLogger().Log(ex.InnerException + "  " + ex.Message, EventLogSeverity.Error);
            }
        }



        // look up customer in Stripe to validate the request
        public bool ValidateCustomer(string customerId)
        {
            StripeCustomer stripeCustomer = StripeFactory.GetStripeService().GetCustomer(customerId);

            if(stripeCustomer!=null)
            {
                return true;             
            }

            return false;
        }
    }
}
