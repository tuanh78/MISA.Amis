using System;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Lớp quy tắc mã của các bảng
    /// </summary>
    /// CreatedDate: 17/06/2021
    /// CreatedBy: PTANH
    public class CodeRule : BaseEntity
    {
        /// <summary>
        /// Id của CodeRule
        /// </summary>
        /// CreatedDate: 17/06/2021
        /// CreatedBy: PTANH
        public Guid CodeRuleId { get; set; }

        /// <summary>
        /// Tiền tố của mã
        /// CreatedDate: 17/06/2021
        /// CreatedBy: PTANH
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Tên bảng
        /// CreatedDate: 17/06/2021
        /// CreatedBy: PTANH
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Số của mã nhân viên mới nhất
        /// CreatedDate: 17/06/2021
        /// CreatedBy: PTANH
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Độ dài của mã
        /// CreatedDate: 17/06/2021
        /// CreatedBy: PTANH
        /// </summary>
        public int Length { get; set; }
    }
}