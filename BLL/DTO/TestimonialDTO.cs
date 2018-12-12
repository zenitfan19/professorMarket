using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class TestimonialDTO
    {
        public TestimonialDTO() { }

        public long id { get; set; }        
        public long requestId { get; set; }        
        public int star { get; set; }
        public string text { get; set; }        
        public System.DateTime date { get; set; }

        public virtual RequestDTO request { get; set; }        
    }
}
