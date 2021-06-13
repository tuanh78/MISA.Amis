using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MySql.Data.MySqlClient;

namespace MISA.AMIS.Infrastructure.Repositories
{
    /// <summary>
    /// Repository của phòng ban
    /// </summary>
    /// CreatedDate: 12/06/2021
    /// CreatedBy: PTANH
    public class DepartmentRepository : BaseRepository<Department>
    {
        #region Hàm khởi tạo

        /// <summary>
        /// Hàm khởi tạo của phòng ban
        /// </summary>
        /// <param name="configuration">Biến để lấy chuỗi kết nối với csdl</param>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
            // Lấy chuỗi kết nối đến csdl
            this._connectionString = configuration.GetConnectionString("DefaultDb");
            // Khởi tạo kết nối đến csdl
            _dbConnection = new MySqlConnection(_connectionString);
        }

        #endregion Hàm khởi tạo
    }
}