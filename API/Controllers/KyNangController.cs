using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KyNangController : Controller
    {
        private readonly IKyNangBusiness _business;
        public KyNangController(IKyNangBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public async Task<IActionResult> LayTatCa()
        {
            var ds = await _business.LayTatCaAsync();
            return Ok(ds);
        }

        [HttpGet("tim-kiem")]
        public async Task<IActionResult> TimKiem([FromQuery] string tuKhoa)
        {
            var ds = await _business.TimKiemAsync(tuKhoa);
            return Ok(ds);
        }
    }
}
