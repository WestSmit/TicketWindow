using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public RouteDTO Route { get; set; }
        public CarriageDTO Wagon { get; set; }
        public int SeatNumber { get; set; }
    }
    
}
