using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public RouteViewModel Route { get; set; }
        public CarriageViewModel Wagon { get; set; }
        public int SeatNumber { get; set; }

    }
}