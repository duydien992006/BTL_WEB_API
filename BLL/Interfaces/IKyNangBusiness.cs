using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public partial interface IKyNangBusiness
    {
        Task<PhanHoiModel> TaoAsync(TaoKyNangModel model);   // đổi tham số
        Task<IEnumerable<KyNangModel>> LayTatCaAsync();
        Task<IEnumerable<KyNangModel>> TimKiemAsync(string tuKhoa);
    }
}
