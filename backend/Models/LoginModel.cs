namespace backend.Models
{
    public class LoginModel
    {
        public class ParamLogin
        {
            public string sUserName { get; set; } = string.Empty;
            public string sPwd { get; set; } = string.Empty;
        }
        public class ResultLogin
        {
            public string sFirstname { get; set; } = string.Empty;
            public string sLastname { get; set; } = string.Empty;
            public string sEmail { get; set; } = string.Empty;
            public string sPhone { get; set; } = string.Empty;
        }
    }
}

//namespace backend.Models.LoginModel
//{
//}
