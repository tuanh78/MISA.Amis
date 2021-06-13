using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repositories;
using System.Data;
using System.Linq;

namespace MISA.AMIS.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public bool CheckDepartmentExists(Employee employee)
        {
            var sql = "Proc_CheckDepartmentExists";
            var parameters = new DynamicParameters();
            parameters.Add("@DepartmentId", employee.DepartmentId);
            var result = _dbConnection.Query<bool>(sql, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return result;
        }

        public bool CheckEmployeeCodeExists(Employee entity)
        {
            var sql = "Proc_CheckEmployeeCodeExists";
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", entity.EmployeeId);
            parameters.Add("@EmployeeCode", entity.EmployeeCode);
            var result = _dbConnection.Query<bool>(sql, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return result;
        }

        public string GetMaxEmployeeCode()
        {
            var sql = "Proc_GetMaxEmployeeCode";
            string maxEmployeeCode = _dbConnection.Query<string>(sql, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return maxEmployeeCode;
        }
    }
}