using System;

namespace MISA.AMIS.Core.Attributes
{
    /// <summary>
    /// Các Attribute validate dữ liệu
    /// </summary>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public class MISAAttribute
    {
        /// <summary>
        /// Check trùng dữ liệu
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public class MISARequired : Attribute
        {
            #region Khai báo biến

            /// <summary>
            /// Tên của thuộc tính
            /// </summary>
            /// CreatedDate: 13/06/2021
            /// CreatedBy: PTANH
            public string PropertyName;

            /// <summary>
            /// Câu thông báo lỗi
            /// </summary>
            /// CreatedDate: 13/06/2021
            /// CreatedBy: PTANH
            public string ErrorMessage;

            #endregion Khai báo biến

            #region Hàm khởi tạo

            /// <summary>
            /// Hàm khởi tạo
            /// </summary>
            /// <param name="propertyName">Tên thuộc tính</param>
            /// <param name="errorMessage">Thông báo lỗi</param>
            /// CreatedDate: 13/06/2021
            /// CreatedBy: PTANH
            public MISARequired(string propertyName, string errorMessage = null)
            {
                this.PropertyName = propertyName;
                this.ErrorMessage = errorMessage;
            }

            #endregion Hàm khởi tạo
        }
    }
}