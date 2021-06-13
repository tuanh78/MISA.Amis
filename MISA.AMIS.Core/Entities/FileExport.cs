using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// File export
    /// </summary>
    /// CreatedDate: 11/6/2021
    /// CreatedBy: PTANH
    public class FileExport
    {
        /// <summary>
        /// Hàm khởi tạo có tham số
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="fileContent"></param>
        /// <param name="fileName"></param>
        /// CreatedDate: 11/6/2021
        /// CreatedBy: PTANH
        public FileExport(MemoryStream fileStream, string fileContent, string fileName)
        {
            FileStream = fileStream;
            FileContent = fileContent;
            FileName = fileName;
        }

        /// <summary>
        /// Stream của file
        /// </summary>
        /// CreatedDate: 11/6/2021
        /// CreatedBy: PTANH
        public MemoryStream FileStream { get; set; }

        /// <summary>
        /// Content file
        /// </summary>
        /// CreatedDate: 11/6/2021
        /// CreatedBy: PTANH
        public string FileContent { get; set; }

        /// <summary>
        /// Tên file
        /// </summary>
        /// CreatedDate: 11/6/2021
        /// CreatedBy: PTANH
        public string FileName { get; set; }
    }
}