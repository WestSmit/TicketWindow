using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Route { get; set; }
        public int WagonNumber { get; set; }
        public int SeatNumber { get; set; }

    }
}
