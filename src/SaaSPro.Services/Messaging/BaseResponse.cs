using SaaSPro.Common;

namespace SaaSPro.Services.Messaging
{
   public class BaseResponse
    {
       public string   Message { get; set; }
       public bool HasError { get; set; }
       public ErrorCode ErrorCode { get; set; }
    }
}
