using System;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Lớp base entiy chứa các thuộc tính dùng chung
    /// </summary>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public class BaseEntity
    {
        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public DateTime ModifiedDate { get; set; }
    }
}