using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repositories;

namespace MISA.AMIS.Core.Services
{
    public class DepartmentService : BaseService<Department>
    {
        public DepartmentService(IBaseRepository<Department> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
    }
}