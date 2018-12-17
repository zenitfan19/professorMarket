using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class RequestForAdminDTO
    {
        public RequestForAdminDTO() { }

        public long id { get; set; }
        public long userId { get; set; }     
        public long adminId { get; set; }
        public long typeId { get; set; }
        public string text { get; set; }
        public string status { get; set; }
        public System.DateTime date { get; set; }

        public RequestForAdminTypeDTO type { get; set; }
        public virtual UserDTO user { get; set; }
        public virtual List<long> documents { get; set; }

    }
}
