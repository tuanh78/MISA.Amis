using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Properties;
using System;
using static MISA.AMIS.Core.Attributes.MISAAttribute;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Đối tượng nhân viên
    /// </summary>
    /// CreatedBy: PTANH (5/6/2021)
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Id nhân viên
        /// </summary>
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MISARequired("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [MISARequired("Tên nhân viên")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Id đơn vị
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        public string EmployeePosition { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public long? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string BankBranchName { get; set; }

        /// <summary>
        /// Tỉnh thành của ngân hàng
        /// </summary>
        public string BankProvinceName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

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