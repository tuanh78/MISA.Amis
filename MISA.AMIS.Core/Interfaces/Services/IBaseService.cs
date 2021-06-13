using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Interfaces.Services
{
    /// <summary>
    /// Interface Service những hàm dùng chung
    /// </summary>
    /// <typeparam name="T">Thực thể cần tương tác</typeparam>
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Lấy danh sách Thực thể
        /// </summary>
        /// <returns>Danh sách thực thể</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public IEnumerable<T> GetEntities();

        /// <summary>
        /// Lấy thực thể theo Id
        /// </summary>
        /// <param name="id">Id của thực thể</param>
        /// <returns>Thực thể cần lấy</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult GetEntityById(Guid id);

        /// <summary>
        /// Thêm thực thể vào csdl
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        /// <returns>Số bản ghi được thực hiện, MISACode, thông báo lỗi nếu có</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH

        public ServiceResult InsertEntity(T entity);

        /// <summary>
        /// Cập nhật thực thể vào csdl
        /// </summary>
        /// <param name="entity">Thực thể cần cập nhật</param>
        /// <param name="id">Id của thực thể</param>
        /// <returns>Số bản ghi bị ảnh hưởng, MISACode, thông báo lỗi nếu có</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult UpdateEntity(T entity, Guid id);

        /// <summary>
        /// Xóa thực thể khỏi csdl
        /// </summary>
        /// <param name="id">Id của thực thể cần xóa</param>
        /// <returns>Só bản ghi bị ảnh hưởng</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult DeleteEntity(Guid id);

        /// <summary>
        /// Lấy danh sách thực thể theo điều kiện
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số thực thể trên một trang</param>
        /// <param name="filter">Điều kiện lọc dữ liệu</param>
        /// <returns>Danh sách thực thể</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult GetEntitiesPaging(int pageIndex, int pageSize, string filter);

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity">Thực thể cần validate</param>
        /// <returns>Đối tượng service result</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult ValidateData(T entity);

        /// <summary>
        /// Validate dữ liệu tùy chỉnh
        /// </summary>
        /// <param name="entity">Đối tượng cần validate</param>
        /// <returns>Đối tượng service result</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult CustomValidate(T entity);

        /// <summary>
        /// Lấy số lượng thực thể theo điều kiện
        /// </summary>
        /// <param name="filter">Điều kiện lọc dữ liệu</param>
        /// <returns>Tổng số lượng thực thể</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public int GetNumberEntities(string filter);
    }
}