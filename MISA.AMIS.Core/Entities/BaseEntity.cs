using System;

namespace MISA.AMIS.Core.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}