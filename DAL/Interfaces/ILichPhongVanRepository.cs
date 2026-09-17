using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public partial interface ILichPhongVanRepository
    {
        Task<int> TaoAsync(LichPhongVanModel model);
        Task<bool> CapNhatKetQuaAsync(int id, string ketQua);
        Task<IEnumerable<LichPhongVanModel>> LayTheoDonUngTuyenAsync(int donUngTuyenId);
    }
}
