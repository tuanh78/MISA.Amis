using MISA.AMIS.Core.Enums;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Lớp chứa kết quả trả về của xử lý nghiệp vụ
    /// </summary>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public class ServiceResult
    {
        /// <summary>
        /// Chứa các mã trả về tương ứng
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public MISACode MISACode { get; set; }

        /// <summary>
        /// Chứa dữ liệu trả về
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public object Data { get; set; }

        /// <summary>
        /// Danh sách các thuộc tính bị lỗi
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public List<PropertyInvalidList> PropertyInvalidLists { get; set; }

        /// <summary>
        /// Thông báo lỗi chung
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string ErrorMessage { get; set; }
    }
}