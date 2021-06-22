using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repositories;
using MISA.AMIS.Core.Interfaces.Services;
using MISA.AMIS.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static MISA.AMIS.Core.Attributes.MISAAttribute;

namespace MISA.AMIS.Core.Services
{
    /// <summary>
    /// Service chứa các hàm dùng chung
    /// </summary>
    /// <typeparam name="T">Thực thể cần tương tác</typeparam>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public class BaseService<T> : IBaseService<T> where T : class
    {
        #region Khai báo biến

        /// <summary>
        /// Khai báo biến của lớp BaseRepository
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public IBaseRepository<T> _baseRepository;

        /// <summary>
        /// Biến chứa kết quả trả về, MISACode, thông báo lỗi nếu có
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH

        private ServiceResult _serviceResult = new();

        #endregion Khai báo biến

        #region Hàm khởi tạo

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="baseRepository">Khởi tạo đối tượng từ class BaseRepository</param>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        #endregion Hàm khởi tạo

        #region Phương thức

        /// <summary>
        /// Hàm xóa thực thể khỏi csdl
        /// </summary>
        /// <param name="id">Id của thực thể cần xóa</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult DeleteEntity(Guid id)
        {
            // Thực hiện xóa thực thể
            var rowAffects = _baseRepository.DeleteEntity(id);
            // Khởi tạo đối tượng chứa kết quả trả về
            _ = new ServiceResult();
            ServiceResult serviceResult;
            // Thành công

            if (rowAffects == 1)
            {
                serviceResult = new ServiceResult { Data = 1, MISACode = MISACode.Success };
            }
            // Thất bại
            else
            {
                serviceResult = new ServiceResult { ErrorMessage = MessageResouce.ErrorDelete, MISACode = MISACode.InvalidValue };
            }
            // Trả về kết quả
            return serviceResult;
        }

        /// <summary>
        /// Lấy danh sách thực thể
        /// </summary>
        /// <returns>Danh sách thực thể</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public IEnumerable<T> GetEntities()
        {
            // Thực hiện lấy danh sách thực thể
            var entities = _baseRepository.GetEntities();
            // Trả về danh sách thực thể
            return entities;
        }

        /// <summary>
        /// Lấy thực thể theo Id
        /// </summary>
        /// <param name="id">Id của thực thể</param>
        /// <returns>Thực thể cần lấy</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult GetEntityById(Guid id)
        {
            // Lấy thực thể theo Id
            var entity = _baseRepository.GetEntityById(id);
            // Khởi tạo đối tượng chứa kết quả trả về
            var serviceResult = new ServiceResult();
            // Không có thực thể nào
            if (entity == null)
            {
                // Gán thông báo lỗi
                serviceResult.ErrorMessage = MessageResouce.NoContent;
                // Gán mã không có nội dung
                serviceResult.MISACode = MISACode.NoContent;
            }
            // Tìm thấy thực thể
            else
            {
                // Gán thực thể vào data
                serviceResult.Data = entity;
                // Mã tìm thấy thành công
                serviceResult.MISACode = MISACode.Success;
            }
            // Trả về kết quả thực hiện
            return serviceResult;
        }

        /// <summary>
        /// Thêm thực thể vào csdl
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        /// <returns>Số lượng bản ghi thực hiện</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult InsertEntity(T entity)
        {
            // Validate dữ liệu
            _serviceResult = ValidateData(entity);
            // Kiểm tra dữ liệu có hợp lệ khay không
            if (_serviceResult.MISACode == MISACode.InvalidValue || _serviceResult.MISACode == MISACode.ValueEmpty)
            {
                // Dữ liệu không hợp lệ
                return _serviceResult;
            }
            // Dữ liệu hợp lệ
            else
            {
                // Thêm thực thể vào csdl
                var rowAffects = _baseRepository.InsertEntity(entity);
                // Khai báo biến chứa dữ liệu trả về
                ServiceResult serviceResult;
                // Kiểm tra xem có thêm thành công hay không
                if (rowAffects == 1)
                {
                    // Cập nhật mã của bảng
                    var codeRule = _baseRepository.GetCodeRule();
                    var tableName = typeof(T).Name;
                    var propertyName = tableName + "Code";
                    var property = typeof(T).GetProperty(propertyName);
                    var entityCode = property.GetValue(entity);
                    var regexItem = new Regex(@"\bNV-\w+");
                    if (regexItem.IsMatch(entityCode.ToString()))
                    {
                        _baseRepository.UpdateValueCodeRule();
                    }
                    // Thêm thành công
                    serviceResult = new ServiceResult { Data = 1, MISACode = MISACode.Success };
                }
                // Thêm thất bại
                else
                {
                    serviceResult = new ServiceResult { Data = 0, ErrorMessage = MessageResouce.ErrorInsert, MISACode = MISACode.InvalidValue };
                }
                // Trả về kết quả thực hiện
                return serviceResult;
            }
        }

        /// <summary>
        /// Cập nhật thực thể vào csdl
        /// </summary>
        /// <param name="entity">Thực thể cần cập nhật dữ liệu</param>
        /// <param name="id">Id của thực thể</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult UpdateEntity(T entity, Guid id)
        {
            // Validate dữ liệu
            _serviceResult = ValidateData(entity);
            // Kiểm tra xem dữ liệu có hợp lệ hay không
            if (_serviceResult.MISACode == MISACode.InvalidValue || _serviceResult.MISACode == MISACode.ValueEmpty)
            {
                // Dữ liệu không hợp lệ
                return _serviceResult;
            }
            // Dữ liệu hợp lệ
            else
            {
                // Cập nhật thực thể
                var rowAffects = _baseRepository.UpdateEntity(entity, id);
                // Kiểm tra xem cập nhật có thành công hay không
                // Cập nhật thành công
                if (rowAffects == 1)
                {
                    _serviceResult.MISACode = MISACode.Success;
                    _serviceResult.Data = MessageResouce.SuccessUpdate;
                }
                //Cập nhật thất bại
                else
                {
                    _serviceResult.MISACode = MISACode.InvalidValue;
                    _serviceResult.Data = MessageResouce.ErrorUpdate;
                }
                // Trả về kết quả thực hiện
                return _serviceResult;
            }
        }

        /// <summary>
        /// Hàm Validate dữ liệu
        /// </summary>
        /// <param name="entity">Thực thể cần validate dữ liệu</param>
        /// <returns>MISACode, thông báo lỗi nếu có</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult ValidateData(T entity)
        {
            // Lấy ra danh sách các thuộc tính của thực thể
            var properties = typeof(T).GetProperties();
            // Khởi tạo một biến chứa kết quả thực hiện
            var serviceResult = new ServiceResult
            {
                // Khởi tạo danh sách thuộc tính không hợp lệ
                PropertyInvalidLists = new List<PropertyInvalidList>()
            };
            // Vòng lặp validate dữ liệu
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                var propValue = property.GetValue(entity);
                // Kiểm tra thuộc tính có bắt buộc nhập hay không
                if (property.IsDefined(typeof(MISARequired), true) && (propValue == null || propValue.ToString() == string.Empty))
                {
                    // Dữ liệu bắt buộc nhập mà lại trống
                    // Lấy các trường của attribute MISARequired
                    var requiredAttribute = property.GetCustomAttributes(typeof(MISARequired), true).FirstOrDefault();
                    // Kiểm tra xem có tồn tại trường nào không
                    // Nếu có
                    if (requiredAttribute != null)
                    {
                        // Lấy tên của thuộc tính do người dùng đặt
                        var propertyName = (requiredAttribute as MISARequired).PropertyName;
                        // Lấy thông báo lỗi
                        var errorMessage = (requiredAttribute as MISARequired).ErrorMessage;
                        // Lấy tên của thuộc tính bị lỗi
                        var errorPropertyName = Char.ToLowerInvariant(property.Name[0]) + property.Name[1..];
                        // Nếu thông báo lỗi = null thì gán thông báo ngược lai thì lấy thông báo lỗi đó
                        var customErrorMessage = errorMessage == null ? $"{propertyName} {MessageResouce.ErrorRequired}" : errorMessage.ToString();
                        // Thêm property lỗi và thông báo lỗi vào danh sách property lỗi
                        serviceResult.PropertyInvalidLists.Add(new PropertyInvalidList { ErrorMessage = customErrorMessage, PropertyName = errorPropertyName });
                        // Gán mã lỗi không hợp lệ
                        serviceResult.MISACode = MISACode.ValueEmpty;
                        // Gán thông báo lỗi chung
                        serviceResult.ErrorMessage = MessageResouce.DataInvalid;
                        // Trả về kết quả thực hiện
                        return serviceResult;
                    }
                }
            }

            // Lấy kết quả thực hiện từ validate dữ liệu tùy chỉnh
            serviceResult = CustomValidate(entity);
            // Trả về kết quả thực hiện
            return serviceResult;
        }

        /// <summary>
        /// Hàm cho phép validate dữ liệu theo ý muốn
        /// </summary>
        /// <param name="entity">Thực thể cần validate dữ liệu</param>
        /// <returns>Kết quả thực hiện</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public virtual ServiceResult CustomValidate(T entity)
        {
            return new ServiceResult();
        }

        /// <summary>
        /// Lấy danh sách thực thể theo điều kiện
        /// </summary>
        /// <param name="pageIndex">Trang hiên tại</param>
        /// <param name="pageSize">Số lượng thực thể trên một trang</param>
        /// <param name="filter">Điều kiện lọc dữ liệu</param>
        /// <returns>Danh sách thực thể</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public ServiceResult GetEntitiesPaging(int pageIndex, int pageSize, string filter)
        {
            // Lấy danh sách thực thể
            var entities = _baseRepository.GetEntitiesPaging(pageIndex, pageSize, filter);
            // Kiểm tra xem danh sách có tồn tại thực thể nào không
            // Không có
            if (!entities.Any())
            {
                // Gán mã không có dữ liệu
                _serviceResult.MISACode = MISACode.NoContent;
            }
            // Có
            else
            {
                // Gán mã thành công
                _serviceResult.MISACode = MISACode.Success;
                // Gán danh sách thực thể cho data
                _serviceResult.Data = entities;
            }
            // Trả về kết quả thực hiện
            return _serviceResult;
        }

        /// <summary>
        /// Số lượng thực thể theo điều kiện
        /// </summary>
        /// <param name="filter">Điều kiện lọc dữ liệu</param>
        /// <returns>Số lượng thực thể</returns>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public int GetNumberEntities(string filter)
        {
            // Lấy số lượng thực thể theo điều kiện
            var totalEntities = _baseRepository.GetNumberEntities(filter);
            // Trả về số lượng thực thể
            return totalEntities;
        }

        /// <summary>
        /// Lấy mã mới của bảng
        /// </summary>
        /// <returns>Mã mới của bảng</returns>
        /// CreatedDate: 17/06/2021
        /// CreatedBy: PTANH
        public string GetNewCode()
        {
            // Lấy nguyên tắc mã của bảng
            var codeRule = _baseRepository.GetCodeRule();
            // Tiền tố của mã
            var prefix = codeRule.Prefix.Trim();
            // Giá trị của mã
            var valueCode = codeRule.Value;
            // Độ dài của số trong chuỗi
            var lengthNumberCode = codeRule.Length - prefix.Length;
            // Mã mới
            var newCode = prefix + valueCode.ToString($"D{lengthNumberCode}");
            return newCode;
        }

        #endregion Phương thức
    }
}