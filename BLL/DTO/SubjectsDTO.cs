using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class SubjectsDTO
    {
        public SubjectsDTO() { }

        public long id { get; set; }
        public string name { get; set; }
        public long subjectTypeId { get; set; }
    }
}
