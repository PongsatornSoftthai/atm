using static backend.Models.CommonModel1;
using static backend.Models.LoginModel;

namespace backend.Services
{
    public interface ILoginService
    {
        ResultApi OnLogin(ParamLogin param);
    }
    public class LoginService : ILoginService
    {
        public LoginService() 
        {

        }
        public ResultApi OnLogin(ParamLogin param)
        {
            ResultLogin objResult = new ResultLogin();
            ResultApi result = new ResultApi();
            if(param.sUserName == "Hello")
            {
                objResult.sFirstname = "Pongsatorn";
                objResult.sLastname = "Boontham";
                objResult.sEmail = "Pongsatorn.b@softthai.co.th";
                objResult.sPhone = "0912345678";
                result.nStatusCode = StatusCodes.Status200OK;
                result.sMessage = "Login Success";
                result.objResult = objResult;
            }
            else
            {
                result.nStatusCode = StatusCodes.Status400BadRequest;
                result.sMessage = "Fail to Login";
            }
            return result;
        }
    }
}
