
namespace Motel.Api.Framework.Response
{
    using Motel.Core;
    using Motel.Core.Enum;
    public class ResponseLogin : IResponse
    {
        public ResponseLogin(MessgeCodeRegistration messgeCode)
        {
            MessageCode = (int)messgeCode;
            Message = CommonHelper.DescriptionEnum(messgeCode);
        }
        public int MessageCode { get; set;}
        public string Message { get; set;}
        public string access_token { get; set;}
        public string refresh_token { get;set;}
    }
}
