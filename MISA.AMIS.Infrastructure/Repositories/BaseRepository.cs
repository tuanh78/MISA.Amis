using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MISA.AMIS.Infrastructure.Repositories
{
    /// <summary>
    /// Repository với những hàm dùng chung
    /// </summary>
    /// <typeparam name="T">Đối tượng cần tương tác</typeparam>
    /// CreatedDate: 12/06/2021
    /// CreatedBy: PTANH
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Khai báo biến

        /// <summary>
        /// Khởi tạo biến chứa chuỗi kết nối với csdl
        /// </summary>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public string _connectionString;

        /// <summary>
        /// Khởi tạo biến đại diện một kết nối mở đến database
        /// </summary>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public MySqlConnection _dbConnection;

        /// <summary>
        /// Tên bảng
        /// </summary>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH

        public string _tableName = typeof(T).Name;

        #endregion Khai báo biến

        #region Hàm khởi tạo

        /// <summary>
        /// Hàm khởi tạo của lớp base tương tác với db
        /// </summary>
        /// <param name="configuration">Biến lấy chuỗi connectionString</param>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public BaseRepository(IConfiguration configuration)
        {
            // lấy chuỗi kết nối đến csdl
            this._connectionString = configuration.GetConnectionString("DefaultDb");
            // Kết nối đến db
            _dbConnection = new MySqlConnection(_connectionString);
        }

        #endregion Hàm khởi tạo

        #region Phương thức

        /// <summary>
        /// Hàm xóa một thực thể khỏi csdl
        /// </summary>
        /// <param name="id">Id của thực thể muốn xóa</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public int DeleteEntity(Guid id)
        {
            // Tên proc xóa thực thể
            var sql = $"Proc_Delete{_tableName}";
            // Biến chứa các tham số đầu vào của Proc
            var parameters = new DynamicParameters();
            // Lưu giá trị của tham số vào biến động
            parameters.Add($"@{_tableName}Id", id, DbType.String);
            // Thực thi Proc
            var rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            // Trả về số lượng bản ghi bị ảnh hưởng
            return rowAffects;
        }

        /// <summary>
        /// Lấy ra danh sách thực thể
        /// </summary>
        /// <returns>Danh sách thực thể</returns>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public IEnumerable<T> GetEntities()
        {
            // Tên proc lấy danh sách thực thể
            var sql = $"Proc_Get{_tableName}s";
            // Thực thi Proc
            var entities = _dbConnection.Query<T>(sql, null, commandType: CommandType.StoredProcedure);
            // Trả về danh sách thực thể
            return entities;
        }

        /// <summary>
        /// Lấy danh sách thực thể theo điều kiện
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi trên trang</param>
        /// <param name="filter">Điều kiện lọc dữ liệu</param>
        /// <returns>Danh sách thực thể</returns>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public IEnumerable<T> GetEntitiesPaging(int pageIndex, int pageSize, string filter)
        {
            // Tên Proc lấy danh sách thực thể theo điều kiện
            var sql = $"Proc_Get{_tableName}sPaging";
            // Biến chứa các tham số của Proc
            var parameters = new DynamicParameters();
            // Thêm tham số trang hiện tại
            parameters.Add("@PageIndex", pageIndex);
            // Thêm tham số số lượng thực thể trên một trang
            parameters.Add("@PageSize", pageSize);
            // Thêm tham số điều kiện lọc dữ liệu
            parameters.Add("@Filter", filter);
            // Thực thi Proc
            var entities = _dbConnection.Query<T>(sql, parameters, commandType: CommandType.StoredProcedure);
            // Trả về danh sách thực thể
            return entities;
        }

        /// <summary>
        /// Lấy thực thể theo Id
        /// </summary>
        /// <param name="id">Id của thực thể</param>
        /// <returns>Thông tin thực thể</returns>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public T GetEntityById(Guid id)
        {
            // Tên Proc lấy thực thể theo Id
            var sql = $"Proc_Get{_tableName}ById";
            // Biến chứa tham số của Proc
            var dynamicParameters = new DynamicParameters();
            // Thêm tham số id
            dynamicParameters.Add($"@{_tableName}Id", id);
            // Thực thi Proc
            var entity = _dbConnection.Query<T>(sql, dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            // Trả về thực thể cần lấy
            return entity;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public CodeRule GetCodeRule()
        {
            // Tên Proc lấy quy tắc mã của bảng
            var sql = $"Proc_Get{_tableName}CodeRule";
            // Biến chứa các tham số của Proc
            var parameters = new DynamicParameters();
            // Tên của bảng
            var tableName = typeof(T).Name;
            parameters.Add("@TableName", tableName);
            // Thực thi Proc
            var codeRule = _dbConnection.Query<CodeRule>(sql, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return codeRule;
        }

        /// <summary>
        /// Lấy số lượng thực thể theo điều kiện
        /// </summary>
        /// <param name="filter">Điều kiện lọc dữ liệu</param>
        /// <returns>Số lượng thực thể có trong db</returns>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public int GetNumberEntities(string filter)
        {
            // Tên Proc lấy số lượng thực thể theo điều kiện
            var sql = $"Proc_GetNumbers{_tableName}s";
            // Biến chứa tham số của Proc
            var parameters = new DynamicParameters();
            // Thêm tham số điều kiện lọc dữ liệu
            parameters.Add("@Filter", filter);
            // Thực thi Proc
            var totalEntities = _dbConnection.Query<int>(sql, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            // Số lượng thực thể thỏa mãn điều kiện lọc
            return totalEntities;
        }

        /// <summary>
        /// Thêm một thực thể vào csdl
        /// </summary>
        /// <param name="entity">Thông tin thực thể cần thêm</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public int InsertEntity(T entity)
        {
            // Tên Proc thêm thực thể vào csdl
            var sql = $"Proc_Insert{_tableName}";
            // Lấy danh sách thuộc tính của đối tượng T
            var properties = typeof(T).GetProperties();
            // Biến chứa tham số của Proc
            var parameters = new DynamicParameters();
            // Vòng lặp xử lý dữ liệu
            foreach (var property in properties)
            {
                // Lấy tên của thuộc tính
                var propertyName = property.Name;
                // Lấy giá trị của thuộc tính
                var propertyValue = property.GetValue(entity);
                // Lấy kiểu của thuộc tính
                var propertyType = property.PropertyType;
                // Kiểm trả kiểu của thuộc tính có phải kiểu Guid không
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    // Có thì chuyển thành string rồi thêm tham số cho Proc
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    // Không thì thêm tham số cho Proc
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }
            // Thực thi Proc
            var rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            // Trả về số lượng bản ghi bị ảnh hưởng
            return rowAffects;
        }

        /// <summary>
        /// Sửa một thực thể trong db
        /// </summary>
        /// <param name="entity">Thực thể cần cập nhật</param>
        /// <param name="id">Id của thực thể cần cập nhật</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedDate: 12/06/2021
        /// CreatedBy: PTANH
        public int UpdateEntity(T entity, Guid id)
        {
            // Tên Proc cập nhật thực thể
            var sql = $"Proc_Update{_tableName}";
            // Lấy danh sách thuộc tính của đối tượng T
            var properties = typeof(T).GetProperties();
            // Biến chứa tham số của Proc
            var parameters = new DynamicParameters();
            // Vòng lặp xử lý dữ liệu
            foreach (var property in properties)
            {
                // Lấy tên của thuộc tính
                var propertyName = property.Name;
                // Lấy giá trị của thuộc tính
                var propertyValue = property.GetValue(entity);
                // Lấy kiểu của thuộc tính
                var propertyType = property.PropertyType;
                // Kiểm trả kiểu của thuộc tính có phải kiểu Guid không
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    // Có thì chuyển thành string rồi thêm tham số cho Proc
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    // Có thì chuyển thành string rồi thêm tham số cho Proc
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            // Thêm tham số Id cho Proc
            parameters.Add($"@{_tableName}Id", id, DbType.String);
            // Thực thi Proc
            var rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            // Trả về số lượng bản ghi bị ảnh hưởng
            return rowAffects;
        }

        /// <summary>
        /// Cập nhật mã của bảng
        /// </summary>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedDate: 17/06/2021
        /// CreatedBy: PTANH
        public int UpdateValueCodeRule()
        {
            // Tên Proc cập nhật mã nhân viên
            var sql = "Proc_UpdateValueCodeRule";
            // Tên bảng
            var tableName = typeof(T).Name;
            // Biến chứa các tham số của Proc
            var parameters = new DynamicParameters();
            parameters.Add("TableName", tableName);
            // Thực thi Proc
            var rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }

        #endregion Phương thức
    }
}