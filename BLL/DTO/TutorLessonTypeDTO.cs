using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class TutorLessonTypeDTO
    {
        public TutorLessonTypeDTO() { }

        public long id { get; set; }
        public long tutorId { get; set; }
        public long lessontypeId { get; set; }        
        public int cost { get; set; }
    }
}
