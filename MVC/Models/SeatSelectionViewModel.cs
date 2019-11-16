using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class SeatSelectionViewModel
    {
        public List<string> SelectedWagons { get; set; }
        public List<string> SelectedSeats { get; set; }
        public RouteViewModel Route { get; set; }
    }
}