using MISA.AMIS.Core.Entities;

namespace MISA.AMIS.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface repository của nhân viên
    /// </summary>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Hàm kiểm tra mã nhân viên đã tồn tại chưa
        /// </summary>
        /// <param name="employee">Nhân viên cần kiểm tra</param>
        /// <returns>Tồn tại thì trả về true ngược lại false</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public bool CheckEmployeeCodeExists(Employee employee);

        /// <summary>
        /// Hàm kiểm tra xem phòng ban có tồn tại hay không
        /// </summary>
        /// <param name="employee">Nhân viên cần kiểm tra</param>
        /// <returns>Tồn tại thì trả về true ngược lại false</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public bool CheckDepartmentExists(Employee employee);
    }
}