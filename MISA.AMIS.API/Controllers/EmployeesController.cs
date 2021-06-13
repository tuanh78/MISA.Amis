using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        public IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            this._employeeService = employeeService;
        }

        // GET: api/<BaseController>
        [HttpGet("max-employee-code")]
        public IActionResult Get()
        {
            var serviceResult = _employeeService.GetMaxEmployeeCode();
            if (serviceResult.MISACode == MISACode.Success)
            {
                return Ok(serviceResult.Data);
            }
            return NoContent();
        }

        // GET: api/<BaseController>
        [HttpGet("Export")]
        public IActionResult Export()
        {
            var result = _employeeService.ExportExcel();
            if (result != null)
            {
                return File(result.FileStream, result.FileContent, result.FileName);
            }
            else
            {
                return NoContent();
            }
        }
    }
}