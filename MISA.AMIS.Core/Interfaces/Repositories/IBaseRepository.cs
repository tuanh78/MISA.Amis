using System;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface base tầng infrastructure tương tác với db
    /// </summary>
    /// <typeparam name="T">Đối tượng thực hiện</typeparam>
    /// CreatedBy: PTANH (5/6/2021)
    public interface IBaseRepository<T> where T : class
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
        public T GetEntityById(Guid id);

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Số bản ghi được thực hiện</returns>
        /// CreatedBy: PTANH (5/6/2021)

        public int InsertEntity(T entity);

        /// <summary>
        /// Cập nhật dữ liệu đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật</param>
        /// <param name="id">Id của đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PTANH (5/6/2021)
        public int UpdateEntity(T entity, Guid id);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="id">Id của đối tượng cần xóa</param>
        /// <returns>Só bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PTANH (5/6/2021)
        public int DeleteEntity(Guid id);

        /// <summary>
        /// Lấy danh sách đối tượng theo trang và lọc dữ liệu
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: PTANH (7/6/2021)
        public IEnumerable<T> GetEntitiesPaging(int pageIndex, int pageSize, string filter);

        /// <summary>
        /// Lấy tổng số lượng bản ghi theo điều kiện
        /// </summary>
        /// <param name="filter">Điều kiện</param>
        /// <returns>Tổng số lượng bản ghi</returns>
        /// CreatedBy: PTANH (13/06/2021)
        public int GetNumberEntities(string filter);
    }
}