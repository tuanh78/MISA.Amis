using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Services;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.AMIS.API.Controllers
{
    /// <summary>
    /// Controller của nhân viên
    /// </summary>
    /// CreatedDate: 14/06/2021
    /// CreatedBy: PTANH
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        #region Khai báo biến

        /// <summary>
        /// Khai báo đối tượng của lớp EmployeeService
        /// </summary>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH
        public IEmployeeService _employeeService;

        #endregion Khai báo biến

        #region Hàm khởi tạo

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="employeeService">Đối tượng của lớp EmployeeService</param>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            this._employeeService = employeeService;
        }

        #endregion Hàm khởi tạo

        #region Phương thức

        /// <summary>
        /// Hàm xuất file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH
        // GET: api/<BaseController>
        [HttpGet("Export")]
        public IActionResult Export()
        {
            // Kết quả xuất file excel
            var result = _employeeService.ExportExcel();
            // Nếu xuất thành công
            if (result != null)
            {
                return File(result.FileStream, result.FileContent, result.FileName);
            }
            // Thất bại
            else
            {
                return NoContent();
            }
        }

        #endregion Phương thức
    }
}