using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace BLL.Interfaces
{
    public partial interface IHoSoBusiness
    {
        Task<PhanHoiModel> TaoAsync(IFormFile file, int ungVienId, int soNamKinhNghiem, List<int> danhSachKyNangId);
        Task<HoSoModel> LayChiTietAsync(int id);
        Task<IEnumerable<HoSoModel>> LayDanhSachTheoUngVienAsync(int ungVienId);
        Task<PhanHoiModel> XoaAsync(int id, int ungVienId);
    }
}
