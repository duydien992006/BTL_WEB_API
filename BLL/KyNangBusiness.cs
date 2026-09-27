using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class KyNangBusiness : IKyNangBusiness
    {
        private readonly IKyNangRepository _repo;
        public KyNangBusiness(IKyNangRepository repo)
        {
            _repo = repo;
        }

        public async Task<PhanHoiModel> TaoAsync(TaoKyNangModel model)
        {
            // Nghiệp vụ: không cho tạo kỹ năng với tên rỗng
            if (string.IsNullOrWhiteSpace(model?.TenKyNang))
            {
                return new PhanHoiModel { ThanhCong = false, ThongDiep = "Tên kỹ năng không được để trống" };
            }

            // Nghiệp vụ: chuẩn hóa dữ liệu trước khi lưu (xóa khoảng trắng thừa ở đầu/cuối)
            string tenKyNang = model.TenKyNang.Trim();

            int id = await _repo.TaoAsync(tenKyNang);
            return new PhanHoiModel { ThanhCong = true, ThongDiep = "Tạo kỹ năng thành công", DuLieu = id };
        }

        public async Task<IEnumerable<KyNangModel>> LayTatCaAsync()
        {
            return await _repo.LayTatCaAsync();
        }

        public async Task<IEnumerable<KyNangModel>> TimKiemAsync(string tuKhoa)
        {
            return await _repo.TimKiemAsync(tuKhoa);
        }
    }
}

