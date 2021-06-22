namespace MISA.AMIS.Core.Enums
{
    /// <summary>
    /// Các loại mã khi thực hiện
    /// </summary>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public enum MISACode
    {
        /// <summary>
        /// Thành công
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        Success = 200,

        /// <summary>
        /// Giá trị không hợp lệ
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        InvalidValue = 400,

        /// <summary>
        /// Dữ liệu bị trùng
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        DuplicateValue = 600,

        /// <summary>
        /// Không có nội dung
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        NoContent = 204,

        /// <summary>
        /// Giá trị để trống yêu cầu nhập lại
        /// CreatedDate: 22/06/2021
        /// CreatedBy: PTANH
        /// </summary>
        ValueEmpty = 650
    }
}