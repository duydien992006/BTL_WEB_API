using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL.Interfaces;

namespace BLL.Interfaces
{
    public partial interface INguoiDungBusiness
    {
        Task<PhanHoiModel> DangKyAsync(DangKyModel model);
        Task<PhanHoiModel> DangNhapAsync(DangNhapModel model);
        Task<PhanHoiModel> CapNhatAsync(CapNhatNguoiDungModel model);
    }
}
