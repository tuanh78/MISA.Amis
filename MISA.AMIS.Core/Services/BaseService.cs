using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repositories;
using MISA.AMIS.Core.Interfaces.Services;
using MISA.AMIS.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using static MISA.AMIS.Core.Attributes.MISAAttribute;

namespace MISA.AMIS.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        public IBaseRepository<T> _baseRepository;
        private ServiceResult _serviceResult = new ServiceResult();

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public ServiceResult DeleteEntity(Guid id)
        {
            var rowAffects = _baseRepository.DeleteEntity(id);
            var serviceResult = new ServiceResult();

            if (rowAffects == 1)
            {
                serviceResult = new ServiceResult { Data = 1, MISACode = MISACode.Success };
            }
            else
            {
                serviceResult = new ServiceResult { ErrorMessage = "Xóa không thành công", MISACode = MISACode.InvalidValue };
            }
            return serviceResult;
        }

        public IEnumerable<T> GetEntities()
        {
            var entities = _baseRepository.GetEntities();
            return entities;
        }

        public ServiceResult GetEntityById(Guid id)
        {
            var entity = _baseRepository.GetEntityById(id);
            var serviceResult = new ServiceResult();

            if (entity == null)
            {
                serviceResult.ErrorMessage = "Không có bản ghi nào";
                serviceResult.MISACode = MISACode.NoContent;
            }
            else
            {
                serviceResult.Data = entity;
                serviceResult.MISACode = MISACode.Success;
            }
            return serviceResult;
        }

        public ServiceResult InsertEntity(T entity)
        {
            _serviceResult = ValidateData(entity);
            if (_serviceResult.MISACode == MISACode.InvalidValue)
            {
                return _serviceResult;
            }
            else
            {
                var rowAffects = _baseRepository.InsertEntity(entity);
                ServiceResult serviceResult;
                if (rowAffects == 1)
                {
                    serviceResult = new ServiceResult { Data = 1, MISACode = MISACode.Success };
                }
                else
                {
                    serviceResult = new ServiceResult { Data = 0, ErrorMessage = "Không thêm được dữ liệu", MISACode = MISACode.InvalidValue };
                }
                return serviceResult;
            }
        }

        public ServiceResult UpdateEntity(T entity, Guid id)
        {
            _serviceResult = ValidateData(entity);
            if (_serviceResult.MISACode == MISACode.InvalidValue)
            {
                return _serviceResult;
            }
            else
            {
                var rowAffects = _baseRepository.UpdateEntity(entity, id);

                if (rowAffects == 1)
                {
                    _serviceResult.MISACode = MISACode.Success;
                    _serviceResult.Data = "Cập nhật thành công";
                }
                else
                {
                    _serviceResult.MISACode = MISACode.InvalidValue;
                    _serviceResult.Data = "Cập nhật không thành công";
                }
                return _serviceResult;
            }
        }

        public ServiceResult ValidateData(T entity)
        {
            var properties = typeof(T).GetProperties();
            var serviceResult = new ServiceResult();
            serviceResult.PropertyInvalidLists = new List<PropertyInvalidList>();
            foreach (var property in properties)
            {
                var propValue = property.GetValue(entity);
                if (property.IsDefined(typeof(MISARequired), true) && (propValue == null || propValue.ToString() == string.Empty))
                {
                    var requiredAttribute = property.GetCustomAttributes(typeof(MISARequired), true).FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        var propertyName = (requiredAttribute as MISARequired).PropertyName;
                        var errorMessage = (requiredAttribute as MISARequired).ErrorMessage;
                        var errorPropertyName = Char.ToLowerInvariant(property.Name[0]) + property.Name.Substring(1);
                        var customErrorMessage = errorMessage == null ? $"{propertyName} {Resources.Error_Required}" : errorMessage.ToString();
                        serviceResult.PropertyInvalidLists.Add(new PropertyInvalidList { ErrorMessage = customErrorMessage, PropertyName = errorPropertyName });
                        serviceResult.MISACode = MISACode.InvalidValue;
                        serviceResult.ErrorMessage = "Dữ liệu không hợp lệ";
                        return serviceResult;
                    }
                }
            }

            serviceResult = CustomValidate(entity);
            return serviceResult;
        }

        public virtual ServiceResult CustomValidate(T entity)
        {
            return new ServiceResult();
        }

        public ServiceResult GetEntitiesPaging(int pageIndex, int pageSize, string filter)
        {
            var entities = _baseRepository.GetEntitiesPaging(pageIndex, pageSize, filter);
            if (entities.Count() == 0)
            {
                _serviceResult.MISACode = MISACode.NoContent;
            }
            else
            {
                _serviceResult.MISACode = MISACode.Success;
                _serviceResult.Data = entities;
            }
            return _serviceResult;
        }

        public int GetNumberEntities(string filter)
        {
            var totalEntities = _baseRepository.GetNumberEntities(filter);
            return totalEntities;
        }
    }
}