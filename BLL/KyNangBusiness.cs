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
