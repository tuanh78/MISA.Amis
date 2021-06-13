using MISA.AMIS.Core.Enums;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Entities
{
    public class ServiceResult
    {
        public MISACode MISACode { get; set; }
        public object Data { get; set; }

        public List<PropertyInvalidList> PropertyInvalidLists { get; set; }
        public string ErrorMessage { get; set; }
    }
}