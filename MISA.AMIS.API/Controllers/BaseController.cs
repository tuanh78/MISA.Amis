using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Services;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.AMIS.API.Controllers
{
    /// <summary>
    /// Base Controller chứa các hàm dùng chung
    /// </summary>
    /// <typeparam name="T">Thực thể cần tương tác</typeparam>
    /// CreatedDate: 14/06/2021
    /// CreatedBy: PTANH
    public class BaseController<T> : ControllerBase where T : class
    {
        #region Khai báo biến

        /// <summary>
        /// Khai báo biến của lớp BaseService
        /// </summary>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH
        public IBaseService<T> _baseService;

        #endregion Khai báo biến

        #region Hàm khởi tạo

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="baseService">Đối tượng của lớp BaseService chứa các hàm dùng chung</param>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH
        public BaseController(IBaseService<T> baseService)
        {
            this._baseService = baseService;
        }

        #endregion Hàm khởi tạo

        #region Phương thức

        /// <summary>
        /// Lấy danh sách thực thể
        /// </summary>
        /// <returns>Danh sách thực thể</returns>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH
        // GET: api/<BaseController>
        [HttpGet]
        public IActionResult Get()
        {
            // Lấy danh sách thực thể
            var entities = _baseService.GetEntities();
            // Trả về danh sách thực thể
            return Ok(entities);
        }

        /// <summary>
        /// Lấy thực thể theo Id
        /// </summary>
        /// <param name="id">Id của thực thể</param>
        /// <returns>Thực thể cần lấy</returns>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH
        // GET api/<BaseController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            // Kết quả lấy thực thể theo Id
            var serviceResult = _baseService.GetEntityById(id);
            // Nếu thành công
            if (serviceResult.MISACode == MISACode.Success)
            {
                return Ok(serviceResult.Data);
            }
            // Không có dữ liệu
            return StatusCode(204, serviceResult);
        }

        /// <summary>
        /// Lấy số lượng thực thể
        /// </summary>
        /// <param name="filter">Điều kiện lọc dữ liệu</param>
        /// <returns>Số lượng thực thể</returns>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH
        // GET api/<BaseController>/5
        [HttpGet("numbers-record")]
        public IActionResult GetNumberEntities([FromQuery] string filter)
        {
            // Số lượng thực thể
            var totalEntities = _baseService.GetNumberEntities(filter);
            // Trả về số lượng thực thể
            return Ok(totalEntities);
        }

        /// <summary>
        /// Thêm thực thể vào csdl
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        /// <returns>Số lượng bản ghi được thực hiện</returns>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH
        // POST api/<BaseController>
        [HttpPost]
        public IActionResult Post([FromBody] T entity)
        {
            // Kết quả thêm thực thể
            var serviceResult = _baseService.InsertEntity(entity);
            // Nếu thành công
            if (serviceResult.MISACode == MISACode.Success)
            {
                return Created("Thêm thành công", serviceResult.Data);
            }
            // Thêm thất bại
            return BadRequest(serviceResult);
        }

        /// <summary>
        /// Sửa thực thể vào csdl
        /// </summary>
        /// <param name="id">Id của thực thể</param>
        /// <param name="entity">Thực thể cần sửa</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH
        // PUT api/<BaseController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] T entity)
        {
            // Kết quả sửa thực thể
            var serviceResult = _baseService.UpdateEntity(entity, id);
            // Trả về kết quả
            // Nếu thành công
            if (serviceResult.MISACode == MISACode.Success)
            {
                return Ok(serviceResult);
            }
            // Thêm thất bại
            return BadRequest(serviceResult);
        }

        /// <summary>
        /// Xóa thực thể khỏi csdl
        /// </summary>
        /// <param name="id">Id của đối tượng cần xóa</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH
        // DELETE api/<BaseController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // Kết quả xóa thực thể khỏi csdl
            var serviceResult = _baseService.DeleteEntity(id);
            // Nếu xóa thành công
            if (serviceResult.MISACode == MISACode.Success)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            // Xóa thất bại
            else
            {
                return StatusCode(400, serviceResult);
            }
        }

        /// <summary>
        /// Lấy danh sách thực thể theo điều kiện
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng thực thể trên một trang</param>
        /// <param name="filter">Điều kiện lọc dữ liệu</param>
        /// <returns>Danh sách thực thể</returns>
        /// CreatedDate: 14/06/2021
        /// CreatedBy: PTANH

        [HttpGet("paging")]
        public IActionResult GetPaging([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string filter)
        {
            // Kết quả lấy danh sách thực thể theo điều kiện
            var serviceResult = _baseService.GetEntitiesPaging(pageIndex, pageSize, filter);
            // Kiểm tra xem có thành công hay không
            // Nếu thành công
            if (serviceResult.MISACode == MISACode.Success)
            {
                return Ok(serviceResult.Data);
            }
            // Không có dữ liệu
            else
            {
                return NoContent();
            }
        }

        #endregion Phương thức
    }
}