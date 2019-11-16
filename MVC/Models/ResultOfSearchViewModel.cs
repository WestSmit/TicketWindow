using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ResultOfSearchViewModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public List<RouteViewModel> FoundRoutes { get; set; }
        

    }
}