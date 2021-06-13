using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repositories;

namespace MISA.AMIS.Core.Services
{
    /// <summary>
    /// Service của phòng ban
    /// </summary>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public class DepartmentService : BaseService<Department>
    {
        #region Hàm khởi tạo

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="baseRepository">Đối tượng của lớp BaseRepository chứa các hàm chung tương tác với csdl</param>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public DepartmentService(IBaseRepository<Department> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        #endregion Hàm khởi tạo
    }
}