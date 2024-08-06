using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static backend.Models.LoginModel;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly ILoginService _iLoginService;
        public LoginController(ILoginService iLoginService)
        {
            _iLoginService = iLoginService;
        }

        [HttpPost]
        public IActionResult OnLogin(ParamLogin param)
        {
            var result = _iLoginService.OnLogin(param);
            return StatusCode(result.nStatusCode, result);
        }
    }
}
