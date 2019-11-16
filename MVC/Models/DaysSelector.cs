using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class DaysSelector
    {
        
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public string[] SelectedDays()
        {
            List<string> result = new List<string>();

            if (Monday == true) result.Add("Monday");
            if (Tuesday == true) result.Add("Tuesday");
            if (Wednesday == true) result.Add("Wednesday");
            if (Thursday == true) result.Add("Thursday");
            if (Friday == true) result.Add("Friday");
            if (Saturday == true) result.Add("Saturday");
            if (Sunday == true) result.Add("Sunday");

            return result.ToArray();
        }
    }
}