using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class RequestDTO
    {
        public RequestDTO() { }

        public long id { get; set; }
        public long tutorId { get; set; }
        public long studentId { get; set; }
        public long subjectId { get; set; }
        public long lessonTypeId { get; set; }
        public string info { get; set; } 
        public string status { get; set; }
        public System.DateTime date { get; set; }   
        
        public virtual TutorDTO tutor { get; set; }
        public virtual StudentDTO student { get; set; }

    }
}
