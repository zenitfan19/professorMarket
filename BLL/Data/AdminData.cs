using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data
{
    public static class AdminData
    {
        public static List<RequestForAdminDTO> displayRequestsForAdmin(long adminId = 9)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbMyRequests = ctx.RequestsForAdmins.Where(s => s.adminId == adminId).Select(mr => new RequestForAdminDTO
                    {
                        id = mr.id,
                        userId = mr.userId,
                        typeId = mr.typeId,                        
                        status = mr.status,
                        text = mr.text,
                        date = mr.date
                    }).ToList();

                    return dbMyRequests;
                }
            }
            catch (Exception ex)
            {
                throw;
                //return -1;
            }

        }

    }
}
