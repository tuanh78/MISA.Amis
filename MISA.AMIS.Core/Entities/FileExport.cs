using System.IO;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// File export
    /// </summary>
    /// CreatedDate: 13/06/2021
    /// CreatedBy: PTANH
    public class FileExport
    {
        #region Hàm khởi tạo

        /// <summary>
        /// Hàm khởi tạo có tham số
        /// </summary>
        /// <param name="fileStream">Stream của file</param>
        /// <param name="fileContent">Nội dung file</param>
        /// <param name="fileName">Tên file</param>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public FileExport(MemoryStream fileStream, string fileContent, string fileName)
        {
            FileStream = fileStream;
            FileContent = fileContent;
            FileName = fileName;
        }

        #endregion Hàm khởi tạo

        /// <summary>
        /// Stream của file
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public MemoryStream FileStream { get; set; }

        /// <summary>
        /// Content file
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string FileContent { get; set; }

        /// <summary>
        /// Tên file
        /// </summary>
        /// CreatedDate: 13/06/2021
        /// CreatedBy: PTANH
        public string FileName { get; set; }
    }
}