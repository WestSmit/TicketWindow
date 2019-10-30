using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CarriageDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int NumberOfPlaces { get; set; }
        public int FreePlaces { get; set; }
    }
}
