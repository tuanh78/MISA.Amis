using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Properties;
using System;
using static MISA.AMIS.Core.Attributes.MISAAttribute;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Lớp nhân viên
    /// </summary>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Id nhân viên
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        [MISARequired("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        [MISARequired("Tên nhân viên")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public Gender Gender { get; set; }

        /// <summary>
        /// Id đơn vị
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH

        [MISARequired("Phòng ban")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH

        public string DepartmentName { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string EmployeePosition { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string Address { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string BankName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string BankBranchName { get; set; }

        /// <summary>
        /// Tỉnh thành của ngân hàng
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string BankProvinceName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string Email { get; set; }

        /// <summary>
        /// Tên giới tính
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string GenderName
        {
            get
            {
                return Gender switch
                {
                    Gender.Female => Properties.GenderResource.Female,
                    Gender.Male => Properties.GenderResource.Male,
                    Gender.Other => Properties.GenderResource.Other,
                    _ => Properties.GenderResource.Unknow,
                };
            }
            set { }
        }
    }
}