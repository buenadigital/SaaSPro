using System.ComponentModel;

namespace SaaSPro.Common
{

    public enum ErrorCode
    {
        [Description("Incorrect plan identifier")] 
        IncorrectPlanIdentifier = 0,

        [Description("Stripe error")] 
        StripeSetupError = 1,

        [Description("Not Found")] 
        NotFound = 2,

        [Description("Plan not found")] 
        PlanNotFound = 3,

        [Description("Customer not found")] 
        CustomerNotFound = 4,

        [Description("Domain already exists")] 
        DomainAlreadyExists = 5,

        [Description("Sign Up Greeting")] 
        SignUpGreeting = 6,
    }

    public enum EmailTemplateCode
    {
        [Description("Sign Up Greeting")]
        SignUpGreeting,

        [Description("Forgot Password")]
        ForgotPassword,

        [Description("Charge Successfull")]
        ChargeSuccessfull,

        [Description("Charge Refunded")]
        ChargeRefunded,

        [Description("Charge Failed")]
        ChargeFailed,

        [Description("Customer Subscription Deleted")]
        CustomerSubscriptionDeleted,

        [Description("Customer Subscription Updated")]
        CustomerSubscriptionUpdated,

        [Description("Contact Request")]
        ContactRequest,

        [Description("None")]
        None
    }
}
