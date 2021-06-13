using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.AMIS.API.Controllers
{
    /// <summary>
    /// API phòng ban
    /// </summary>
    /// CreatedBy: PTANH (09/6/2021)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<Department>
    {
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="baseService">Lớp base xử lý nghiệp vụ</param>
        /// CreatedBy: PTANH (09/6/2021)
        public DepartmentsController(IBaseService<Department> baseService) : base(baseService)
        {
        }
    }
}