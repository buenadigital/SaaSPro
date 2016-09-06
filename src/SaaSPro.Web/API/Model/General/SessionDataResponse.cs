namespace SaaSPro.Web.API.Model.General
{
    public class SessionDataResponse : SessionResponse
    {
        public object Data { get; set; }

        public SessionDataResponse()
        {
            Success = true;
        }
    }
}