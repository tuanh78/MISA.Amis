using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repositories;
using System.Data;
using System.Linq;

namespace MISA.AMIS.Infrastructure.Repositories
{
    /// <summary>
    /// Repository của nhân viên
    /// </summary>
    /// CreatedDate: 12/06/2021
    /// CreatedBy: PTANH
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Hàm khởi tạo

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration">Biến để lấy chuỗi kết nối với csdl</param>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        #endregion Hàm khởi tạo

        #region Phương thức

        /// <summary>
        /// Hàm kiểm tra phòng ban có tồn tại hay không
        /// </summary>
        /// <param name="employee">Thực thể nhân viên</param>
        /// <returns>Tồn tại thì trả về true ngược lại false</returns>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public bool CheckDepartmentExists(Employee employee)
        {
            // Tên Proc kiểm tra phòng ban có tồn tại hay không
            var sql = "Proc_CheckDepartmentExists";
            // Biến chứa tham số của Proc
            var parameters = new DynamicParameters();
            // Thêm tham số Id của phòng ban
            parameters.Add("@DepartmentId", employee.DepartmentId);
            // Thực thi Proc
            var result = _dbConnection.Query<bool>(sql, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            // Trả về true hoặc false
            return result;
        }

        /// <summary>
        /// Kiểm tra mã khách hàng đã tồn tại hay chưa
        /// </summary>
        /// <param name="entity">Thực thể khách hàng cần kiểm tra</param>
        /// <returns>Tồn tại thì trả về true ngược lại false</returns>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public bool CheckEmployeeCodeExists(Employee entity)
        {
            // Tên Proc kiểm tra mã nhân viên đã tồn tại hay chưa
            var sql = "Proc_CheckEmployeeCodeExists";
            // Biến chứa tham số của Proc
            var parameters = new DynamicParameters();
            // Thêm tham số Id của nhân viên
            parameters.Add("@EmployeeId", entity.EmployeeId);
            // Thêm tham số mã của nhân viên
            parameters.Add("@EmployeeCode", entity.EmployeeCode);
            // Thực thi Proc
            var result = _dbConnection.Query<bool>(sql, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            // Trả về đúng hoặc sai
            return result;
        }

        #endregion Phương thức
    }
}