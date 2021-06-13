using MISA.AMIS.Core.Entities;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Services
{
    /// <summary>
    /// Interface service của nhân viên
    /// </summary>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Lấy ra mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult GetMaxEmployeeCode();

        /// <summary>
        /// Xuất list ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        FileExport ExportExcel();
    }
}