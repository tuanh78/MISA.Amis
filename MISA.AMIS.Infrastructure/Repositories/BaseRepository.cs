using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Interfaces.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MISA.AMIS.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public string _connectionString;
        public MySqlConnection _dbConnection;
        public string _tableName = typeof(T).Name;

        public BaseRepository(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("DefaultDb");

            _dbConnection = new MySqlConnection(_connectionString);
        }

        public int DeleteEntity(Guid id)
        {
            var sql = $"Proc_Delete{_tableName}";
            var parameters = new DynamicParameters();
            parameters.Add($"@{_tableName}Id", id, DbType.String);
            var rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }

        public IEnumerable<T> GetEntities()
        {
            var sql = $"Proc_Get{_tableName}s";
            var entities = _dbConnection.Query<T>(sql, null, commandType: CommandType.StoredProcedure);
            return entities;
        }

        public IEnumerable<T> GetEntitiesPaging(int pageIndex, int pageSize, string filter)
        {
            var sql = $"Proc_Get{_tableName}sPaging";
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", pageIndex);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@Filter", filter);
            var entities = _dbConnection.Query<T>(sql, parameters, commandType: CommandType.StoredProcedure);
            return entities;
        }

        public T GetEntityById(Guid id)
        {
            var sql = $"Proc_Get{_tableName}ById";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{_tableName}Id", id);
            var entity = _dbConnection.Query<T>(sql, dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return entity;
        }

        public int GetNumberEntities(string filter)
        {
            var sql = $"Proc_GetNumbers{_tableName}s";
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", filter);
            var totalEntities = _dbConnection.Query<int>(sql, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return totalEntities;
        }

        public int InsertEntity(T entity)
        {
            var sql = $"Proc_Insert{_tableName}";
            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            var rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }

        public int UpdateEntity(T entity, Guid id)
        {
            var sql = $"Proc_Update{_tableName}";
            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            parameters.Add($"@{_tableName}Id", id, DbType.String);

            var rowAffects = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }
    }
}