using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Messaging.PlanService
{
    public class PlanSignUpRequest
    {
        public SignUpModel SignUpModel { get; set; }
        public string PlanName { get; set; }

        public PlanSignUpRequest()
        {
            SignUpModel=new SignUpModel();
        }
    }
}
