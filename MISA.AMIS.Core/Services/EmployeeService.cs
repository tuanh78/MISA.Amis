using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repositories;
using MISA.AMIS.Core.Interfaces.Services;
using MISA.AMIS.Core.Properties;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeRepository) : base(baseRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public override ServiceResult CustomValidate(Employee entity)
        {
            // Kiểm tra mã nhân viên đã tồn tại hay chưa
            var checkEmployeeCode = _employeeRepository.CheckEmployeeCodeExists(entity);
            // Khởi tạo service result
            var serviceResult = new ServiceResult();
            serviceResult.PropertyInvalidLists = new List<PropertyInvalidList>();
            // Nếu mã nhân viên đã tồn tại
            if (checkEmployeeCode != false)
            {
                var propertyInvalid = new PropertyInvalidList() { ErrorMessage = Resources.Error_EmployeeCodeDuplicate, PropertyName = "employeeCode" };
                serviceResult.PropertyInvalidLists.Add(propertyInvalid);
                serviceResult.ErrorMessage = Resources.Error_EmployeeCodeDuplicate; serviceResult.MISACode = MISACode.InvalidValue;
                return serviceResult;
            }
            // Kiểm tra email có đúng định dạng hay không
            bool checkEmail;
            try
            {
                var addr = new System.Net.Mail.MailAddress(entity.Email);
                checkEmail = addr.Address == entity.Email;
            }
            catch
            {
                checkEmail = false;
            }
            if (checkEmail == false && entity.Email != null && entity.Email != string.Empty)
            {
                var propertyInvalid = new PropertyInvalidList() { ErrorMessage = Resources.Error_EmailFormat, PropertyName = "email" };
                serviceResult.PropertyInvalidLists.Add(propertyInvalid);
                serviceResult.ErrorMessage = Resources.Error_EmailFormat;
                serviceResult.MISACode = MISACode.InvalidValue;
                return serviceResult;
            }

            bool checkDepartmentExists = _employeeRepository.CheckDepartmentExists(entity);
            if (checkDepartmentExists == false)
            {
                var propertyInvalid = new PropertyInvalidList() { ErrorMessage = Resources.Error_DepartmentNotExists, PropertyName = "departmentId" };
                serviceResult.PropertyInvalidLists.Add(propertyInvalid);
                serviceResult.ErrorMessage = Resources.Error_DepartmentNotExists;
                serviceResult.MISACode = MISACode.InvalidValue;
                return serviceResult;
            }
            serviceResult.MISACode = MISACode.Success;
            return serviceResult;
        }

        /// <summary>
        /// Xuất list ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PTANH (11/06/2021)
        public FileExport ExportExcel()
        {
            //var list = await _employeeRepository.GetPaging(request);
            var list = _employeeRepository.GetEntities();
            //var list = res.ToList();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(ExcelExportResource.TITLE);

                workSheet.Column(1).Width = 5;
                workSheet.Column(2).Width = 15;
                workSheet.Column(3).Width = 30;
                workSheet.Column(4).Width = 10;
                workSheet.Column(5).Width = 15;
                workSheet.Column(6).Width = 30;
                workSheet.Column(7).Width = 30;
                workSheet.Column(8).Width = 15;
                workSheet.Column(9).Width = 30;

                using (var range = workSheet.Cells[ExcelExportResource.FIRST_ROW])
                {
                    range.Merge = true;
                    range.Value = ExcelExportResource.TITLE;
                    range.Style.Font.Bold = true;
                    range.Style.Font.Size = 16;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // style cho excel.
                workSheet.Cells[3, 1].Value = ExcelExportResource.COLUMN_INDEX;
                workSheet.Cells[3, 2].Value = ExcelExportResource.COLUMN_CODE;
                workSheet.Cells[3, 3].Value = ExcelExportResource.COLUMN_NAME;
                workSheet.Cells[3, 4].Value = ExcelExportResource.COLUMN_GENDER;
                workSheet.Cells[3, 5].Value = ExcelExportResource.COLUMN_DOB;
                workSheet.Cells[3, 6].Value = ExcelExportResource.COLUMN_POSITION;
                workSheet.Cells[3, 7].Value = ExcelExportResource.COLUMN_DEPARTMENT;
                workSheet.Cells[3, 8].Value = ExcelExportResource.COLUMN_BANK_ACCOUNT_NUMBER;
                workSheet.Cells[3, 9].Value = ExcelExportResource.COLUMN_BANK_NAME;

                using (var range = workSheet.Cells[ExcelExportResource.THIRD_ROW])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    range.Style.Font.Bold = true;
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                int i = 0;
                // đổ dữ liệu từ list vào.
                foreach (var e in list)
                {
                    workSheet.Cells[i + 4, 1].Value = i + 1;
                    workSheet.Cells[i + 4, 2].Value = e.EmployeeCode;
                    workSheet.Cells[i + 4, 3].Value = e.EmployeeName;
                    workSheet.Cells[i + 4, 4].Value = e.GenderName;
                    workSheet.Cells[i + 4, 5].Value = e.DateOfBirth != null ? e.DateOfBirth?.ToString(ExcelExportResource.DATE_FORMAT) : string.Empty;
                    workSheet.Cells[i + 4, 6].Value = e.EmployeePosition;
                    workSheet.Cells[i + 4, 7].Value = e.DepartmentName;
                    workSheet.Cells[i + 4, 8].Value = e.BankAccountNumber;
                    workSheet.Cells[i + 4, 9].Value = e.BankName;

                    using (var range = workSheet.Cells[i + 4, 1, i + 4, 9])
                    {
                        range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }

                    i++;
                }

                package.Save();
                stream.Position = 0;
                //string excelName = $"Employee-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                string excelName = ExcelExportResource.FILE_NAME;
                return new FileExport(stream, ExcelExportResource.FILE_TYPE, excelName);
            }
        }

        public ServiceResult GetMaxEmployeeCode()
        {
            string maxEmployeeCode = _employeeRepository.GetMaxEmployeeCode().Trim();
            var serviceResult = new ServiceResult();
            if (maxEmployeeCode != null || maxEmployeeCode != string.Empty)
            {
                string[] strEmployeeCode = maxEmployeeCode.Split('-');
                int numberEmployee;
                int.TryParse(strEmployeeCode[1], out numberEmployee);
                numberEmployee++;
                string employeeCode = strEmployeeCode[0] + '-' + numberEmployee.ToString();
                serviceResult.Data = employeeCode;
                serviceResult.MISACode = MISACode.Success;
            }
            else
            {
                serviceResult.Data = maxEmployeeCode;
                serviceResult.MISACode = MISACode.NoContent;
                serviceResult.ErrorMessage = Resources.EmployeeCode_NotExists;
            }
            return serviceResult;
        }
    }
}