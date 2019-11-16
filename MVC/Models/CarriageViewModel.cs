using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class CarriageViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Type { get; set; }
        public int NumberOfPlaces { get; set; }
        public int FreePlaces { get; set; }
        public int RouteId { get; set; }
    }
}
