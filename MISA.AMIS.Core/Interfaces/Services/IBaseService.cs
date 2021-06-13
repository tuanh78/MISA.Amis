using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Interfaces.Services
{
    /// <summary>
    /// Interface của tầng xử lý nghiệp vụ
    /// </summary>
    /// <typeparam name="T">Đối tượng cần tương tác</typeparam>
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Lấy danh sách đối tượng
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: PTANH (5/6/2021)
        public IEnumerable<T> GetEntities();

        /// <summary>
        /// Lấy đổi tượng theo Id
        /// </summary>
        /// <param name="id">Id của đối tượng</param>
        /// <returns>Đối tượng cần lấy</returns>
        /// CreatedBy: PTANH (5/6/2021)
        public ServiceResult GetEntityById(Guid id);

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Số bản ghi được thực hiện</returns>
        /// CreatedBy: PTANH (5/6/2021)

        public ServiceResult InsertEntity(T entity);

        /// <summary>
        /// Cập nhật dữ liệu đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật</param>
        /// <param name="id">Id của đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PTANH (5/6/2021)
        public ServiceResult UpdateEntity(T entity, Guid id);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="id">Id của đối tượng cần xóa</param>
        /// <returns>Só bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PTANH (5/6/2021)
        public ServiceResult DeleteEntity(Guid id);

        /// <summary>
        /// Lấy danh sách đối tượng theo trang và lọc dữ liệu
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: PTANH (7/6/2021)
        public ServiceResult GetEntitiesPaging(int pageIndex, int pageSize, string filter);

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng cần validate</param>
        /// <returns>Đối tượng service result</returns>
        /// CreatedBy: PTANH (7/6/2021)
        public ServiceResult ValidateData(T entity);

        /// <summary>
        /// Validate dữ liệu tùy chỉnh
        /// </summary>
        /// <param name="entity">Đối tượng cần validate</param>
        /// <returns>Đối tượng service result</returns>
        /// CreatedBy: PTANH (7/6/2021)
        public ServiceResult CustomValidate(T entity);

        /// <summary>
        /// Lấy tổng số lượng bản ghi theo điều kiện
        /// </summary>
        /// <param name="filter">Điều kiện</param>
        /// <returns>Tổng số lượng bản ghi</returns>
        /// CreatedBy: PTANH (13/06/2021)
        public int GetNumberEntities(string filter);
    }
}