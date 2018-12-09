using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace professorMarketWebUI.Models
{
    public class RequestModel
    {
        public long tutorId { get; set; }
        public long studentId { get; set; }
        public long subjectId { get; set; }
        public long lessonTypeId { get; set; }
        public string info { get; set; }
        public string status { get; set; }
        public System.DateTime date { get; set; }
    }
}