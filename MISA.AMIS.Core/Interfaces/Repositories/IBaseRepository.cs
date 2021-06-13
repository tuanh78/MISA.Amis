using System;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface Repository với những hàm dùng chung
    /// </summary>
    /// <typeparam name="T">Thực thể cần tương tác</typeparam>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Lấy danh sách thực thể
        /// </summary>
        /// <returns>Danh sách thực thể</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public IEnumerable<T> GetEntities();

        /// <summary>
        /// Lấy thực thể theo Id
        /// </summary>
        /// <param name="id">Id của thực thể</param>
        /// <returns>Thực thể cần lấy theo Id</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public T GetEntityById(Guid id);

        /// <summary>
        /// Thêm thực thể vào csdl
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        /// <returns>Số bản ghi được thực hiện</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH

        public int InsertEntity(T entity);

        /// <summary>
        /// Cập nhật thực thể vào csdl
        /// </summary>
        /// <param name="entity">Thực thể cần cập nhật</param>
        /// <param name="id">Id của thực thể</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public int UpdateEntity(T entity, Guid id);

        /// <summary>
        /// Xóa thực thể khỏi csdl
        /// </summary>
        /// <param name="id">Id của thực thể cần xóa</param>
        /// <returns>Só bản ghi bị ảnh hưởng</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public int DeleteEntity(Guid id);

        /// <summary>
        /// Lấy danh sách thực thể theo điều kiện
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng thực thể trên một trang</param>
        /// <param name="filter">Điều kiện lọc dữ liệu</param>
        /// <returns>Danh sách thực thể</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH

        public IEnumerable<T> GetEntitiesPaging(int pageIndex, int pageSize, string filter);

        /// <summary>
        /// Lấy số lượng thực thể theo điều kiện
        /// </summary>
        /// <param name="filter">Điều kiện lọc dữ liệu</param>
        /// <returns>Số lượng bản ghi</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public int GetNumberEntities(string filter);
    }
}