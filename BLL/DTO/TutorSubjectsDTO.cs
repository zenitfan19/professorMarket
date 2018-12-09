using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class TutorSubjectsDTO
    {
        public TutorSubjectsDTO() { }

        public long id { get; set; }
        public long tutorId { get; set; }
        public long subjectId { get; set; } 
        public long subjectTypeId { get; set; }
        public string subjectName { get; set; }
    }
}
