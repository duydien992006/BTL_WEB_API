using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeNghiTuyenDungController : Controller
    {
        private readonly IDeNghiTuyenDungBusiness _business;
        public DeNghiTuyenDungController(IDeNghiTuyenDungBusiness business)
        {
            _business = business;
        }

        [HttpPost("gui")]
        [Authorize(Roles = "NhaTuyenDung")]
        public async Task<IActionResult> Gui([FromBody] DeNghiTuyenDungModel model)
        {
            var kq = await _business.GuiAsync(model);
            if (kq.ThanhCong) return Ok(kq);
            return BadRequest(kq);
        }

        [HttpPut("{id:int}/phan-hoi")]
        [Authorize(Roles = "UngVien")]
        public async Task<IActionResult> PhanHoi(int id, [FromQuery] bool chapNhan)
        {
            var kq = await _business.PhanHoiAsync(id, chapNhan);
            if (kq.ThanhCong) return Ok(kq);
            return NotFound(kq);
        }

        [HttpGet("theo-don/{donUngTuyenId:int}")]
        [Authorize]
        public async Task<IActionResult> LayTheoDon(int donUngTuyenId)
        {
            var ds = await _business.LayTheoDonUngTuyenAsync(donUngTuyenId);
            return Ok(ds);
        }
    }
}
