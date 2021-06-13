using MISA.AMIS.Core.Entities;

namespace MISA.AMIS.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        public bool CheckEmployeeCodeExists(Employee employee);

        public bool CheckDepartmentExists(Employee employee);

        public string GetMaxEmployeeCode();
    }
}