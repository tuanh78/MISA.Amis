namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Lớp thuộc tính không hợp lệ
    /// </summary>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public class PropertyInvalidList
    {
        /// <summary>
        /// Tên thuộc tính
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string PropertyName { get; set; }

        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string ErrorMessage { get; set; }
    }
}