using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Services;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.AMIS.API.Controllers
{
    public class BaseController<T> : ControllerBase where T : class
    {
        public IBaseService<T> _baseService;

        public BaseController(IBaseService<T> baseService)
        {
            this._baseService = baseService;
        }

        // GET: api/<BaseController>
        [HttpGet]
        public IActionResult Get()
        {
            var entities = _baseService.GetEntities();
            return Ok(entities);
        }

        // GET api/<BaseController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var serviceResult = _baseService.GetEntityById(id);
            if (serviceResult.MISACode == MISACode.Success)
            {
                return Ok(serviceResult.Data);
            }
            return StatusCode(204, serviceResult);
        }

        // GET api/<BaseController>/5
        [HttpGet("numbers-record")]
        public IActionResult GetNumberEntities([FromQuery] string filter)
        {
            var totalEntities = _baseService.GetNumberEntities(filter);
            return Ok(totalEntities);
        }

        // POST api/<BaseController>
        [HttpPost]
        public IActionResult Post([FromBody] T entity)
        {
            var serviceResult = _baseService.InsertEntity(entity);
            if (serviceResult.MISACode == MISACode.Success)
            {
                return Created("Thêm thành công", serviceResult.Data);
            }
            return BadRequest(serviceResult);
        }

        // PUT api/<BaseController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] T entity)
        {
            var serviceResult = _baseService.UpdateEntity(entity, id);
            return Ok(serviceResult);
        }

        // DELETE api/<BaseController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var serviceResult = _baseService.DeleteEntity(id);
            if (serviceResult.MISACode == MISACode.Success)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(400, serviceResult);
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo trang, kích thước trang, và lọc dữ liệu
        /// </summary>
        /// <returns>Trả về danh sách nhân viên</returns>
        [HttpGet("paging")]
        public IActionResult GetPaging([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string filter)
        {
            var serviceResult = _baseService.GetEntitiesPaging(pageIndex, pageSize, filter);
            if (serviceResult.MISACode == MISACode.Success)
            {
                return Ok(serviceResult.Data);
            }
            else
            {
                return NoContent();
            }
        }
    }
}