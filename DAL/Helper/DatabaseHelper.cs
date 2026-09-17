using DAL.Helper.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
    public class DatabaseHelper: IDatabaseHelper
    {
        //private readonly string _connectionString;

        //public DatabaseHelper(IConfiguration configuration)
        //{
        //    _connectionString = configuration.GetConnectionString("DefaultConnection");
        //}

        //public T QuerySingleOrDefault<T>(string spName, DynamicParameters parameters = null)
        //{
        //    using (IDbConnection db = new SqlConnection(_connectionString))
        //    {
        //        return db.QuerySingleOrDefault<T>(
        //            spName,
        //            parameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        //public IEnumerable<T> Query<T>(string spName, DynamicParameters parameters = null)
        //{
        //    using (IDbConnection db = new SqlConnection(_connectionString))
        //    {
        //        return db.Query<T>(
        //            spName,
        //            parameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        //public int Execute(string spName, DynamicParameters parameters = null)
        //{
        //    using (IDbConnection db = new SqlConnection(_connectionString))
        //    {
        //        return db.Execute(
        //            spName,
        //            parameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}


        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string spName, object param = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(spName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string spName, object param = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(spName, param, commandType: CommandType.StoredProcedure);
            }
        }

        // Các SP UPDATE/DELETE của mình đều SELECT @@ROWCOUNT ở cuối,
        // nên dùng ExecuteScalarAsync<int> mới đọc được giá trị này
        public async Task<int> ExecuteAsync(string spName, object param = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteScalarAsync<int>(spName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string spName, object param = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteScalarAsync<T>(spName, param, commandType: CommandType.StoredProcedure);
            }
        }

        // Dùng cho SP trả về 2 tập kết quả (VD: Tin + Danh sách kỹ năng)
        public async Task<KetQuaKep<T1, T2>> QueryMultipleAsync<T1, T2>(string spName, object param = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync(spName, param, commandType: CommandType.StoredProcedure))
                {
                    var ketQua1 = await multi.ReadFirstOrDefaultAsync<T1>();
                    var ketQua2 = (await multi.ReadAsync<T2>()).ToList();

                    return new KetQuaKep<T1, T2>
                    {
                        KetQua1 = ketQua1,
                        KetQua2 = ketQua2
                    };
                }
            }
        }

        // Chuyển List<int> thành DataTable để truyền vào Table-Valued Parameter (TVP)
        public DataTable TaoBangId(IEnumerable<int> danhSachId)
        {
            var bang = new DataTable();
            bang.Columns.Add("Id", typeof(int));

            if (danhSachId != null)
            {
                foreach (var id in danhSachId)
                {
                    bang.Rows.Add(id);
                }
            }
            return bang;
        }
    }
}
