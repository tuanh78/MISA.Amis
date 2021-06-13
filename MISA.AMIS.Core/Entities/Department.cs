using System;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Đối tượng đơn vị
    /// </summary>
    /// CreatedBy: PTANH (5/6/2021)
    public class Department : BaseEntity
    {
        /// <summary>
        /// Id đơn vị
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
    }
}