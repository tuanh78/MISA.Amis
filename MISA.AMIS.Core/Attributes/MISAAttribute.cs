using System;

namespace MISA.AMIS.Core.Attributes
{
    /// <summary>
    /// Các Attribute validate dữ liệu
    /// </summary>
    /// CreatedBy: PTANH (7/6/2021)
    public class MISAAttribute
    {
        /// <summary>
        /// Check trùng dữ liệu
        /// </summary>
        public class MISARequired : Attribute
        {
            /// <summary>
            /// Tên của thuộc tính
            /// </summary>
            public string PropertyName;

            /// <summary>
            /// Câu thông báo lỗi
            /// </summary>
            public string ErrorMessage;

            public MISARequired(string propertyName, string errorMessage = null)
            {
                this.PropertyName = propertyName;
                this.ErrorMessage = errorMessage;
            }
        }
    }
}