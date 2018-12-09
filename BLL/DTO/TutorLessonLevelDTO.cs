using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class TutorLessonLevelDTO
    {
        public TutorLessonLevelDTO() { }
        
        public long id { get; set; }
        public long tutorId { get; set; }
        public long lessonlevelId { get; set; }        
        public int cost { get; set; }
    }
}
