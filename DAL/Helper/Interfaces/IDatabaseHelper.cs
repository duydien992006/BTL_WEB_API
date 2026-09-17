using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper.Interfaces
{
    //public class StoreParameterInfo
    //{
    //    public string StoreProcedureName { get; set; }
    //    public List<Object> StoreProcedureParams { get; set; }
    //}
    //public interface IDatabaseHelper
    //{
    //    // 1. Hàm lấy 1 dòng dữ liệu (hoặc 1 giá trị đơn lẻ như Id)
    //    T QuerySingleOrDefault<T>(string spName, DynamicParameters parameters = null);

    //    // 2. Hàm lấy danh sách dữ liệu (nhiều dòng)
    //    IEnumerable<T> Query<T>(string spName, DynamicParameters parameters = null);

    //    // 3. Hàm thực thi (Thêm/Sửa/Xóa) trả về số dòng bị ảnh hưởng
    //    int Execute(string spName, DynamicParameters parameters = null);
    //}
    public interface IDatabaseHelper
    {
        Task<IEnumerable<T>> QueryAsync<T>(string spName, object param = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string spName, object param = null);
        Task<int> ExecuteAsync(string spName, object param = null);
        Task<T> ExecuteScalarAsync<T>(string spName, object param = null);
        Task<KetQuaKep<T1, T2>> QueryMultipleAsync<T1, T2>(string spName, object param = null);
        DataTable TaoBangId(IEnumerable<int> danhSachId);
    }

    // C# 7.3 hỗ trợ ValueTuple nhưng để rõ ràng, dùng 1 class chứa 2 kết quả thay vì tuple ẩn danh phức tạp
    public class KetQuaKep<T1, T2>
    {
        public T1 KetQua1 { get; set; }
        public List<T2> KetQua2 { get; set; }
    }
}
