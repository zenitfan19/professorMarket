using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace professorMarketWebUI.Models
{
    public class TestimonialModel
    {
        public long id { get; set; }
        public long requestId { get; set; }
        public int star { get; set; }        
        public string text { get; set; }        
        public System.DateTime date { get; set; }
    }
}