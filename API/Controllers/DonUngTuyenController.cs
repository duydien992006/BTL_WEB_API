using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonUngTuyenController : Controller
    {
        private readonly IDonUngTuyenBusiness _business;
        public DonUngTuyenController(IDonUngTuyenBusiness business)
        {
            _business = business;
        }

        [HttpPost("nop-don")]
        [Authorize(Roles = "UngVien")]
        public async Task<IActionResult> NopDon([FromBody] NopDonModel model)
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            int ungVienId = claim != null ? int.Parse(claim.Value) : 0;
            var kq = await _business.NopDonAsync(model, ungVienId);
            if (kq.ThanhCong) return Ok(kq);
            return BadRequest(kq);
        }

        [HttpPut("doi-trang-thai")]
        [Authorize(Roles = "NhaTuyenDung")]
        public async Task<IActionResult> DoiTrangThai([FromBody] DoiTrangThaiDonModel model)
        {
            var kq = await _business.DoiTrangThaiAsync(model);
            if (kq.ThanhCong) return Ok(kq);
            return BadRequest(kq);
        }

        [HttpGet("theo-tin/{tinId:int}")]
        [Authorize(Roles = "NhaTuyenDung")]
        public async Task<IActionResult> LayTheoTin(int tinId)
        {
            var ds = await _business.LayTheoTinAsync(tinId);
            return Ok(ds);
        }

        [HttpGet("cua-toi")]
        [Authorize(Roles = "UngVien")]
        public async Task<IActionResult> DonCuaToi()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            int ungVienId = claim != null ? int.Parse(claim.Value) : 0;
            var ds = await _business.LayTheoUngVienAsync(ungVienId);
            return Ok(ds);
        }
    }
}
