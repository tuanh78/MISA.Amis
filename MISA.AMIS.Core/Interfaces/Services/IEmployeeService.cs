using MISA.AMIS.Core.Entities;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Services
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Lấy ra mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// CreatedBy: PTANH ( 11/06/2021)
        public ServiceResult GetMaxEmployeeCode();

        /// <summary>
        /// Xuất list ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PTANH ( 11/06/2021)
        FileExport ExportExcel();
    }
}