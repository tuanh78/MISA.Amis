using System;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Lớp phòng ban
    /// </summary>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public class Department : BaseEntity
    {
        /// <summary>
        /// Id đơn vị
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string Description { get; set; }
    }
}