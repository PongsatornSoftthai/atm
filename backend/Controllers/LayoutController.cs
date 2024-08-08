using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static backend.Models.LayoutModel;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LayoutController : ControllerBase
    {
        readonly ILayoutService _iLayoutService;
        public LayoutController(ILayoutService iLayoutService)
        {
            _iLayoutService = iLayoutService;
        }
        [HttpGet]
        public IActionResult GetMenu()
        {
            var result = _iLayoutService.GetMenu();
            return StatusCode(result.nStatusCode, result);
        }
    }
}
