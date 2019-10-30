using System;
using System.Collections.Generic;
using DAL.Entities;

namespace BLL.DTO
{
    public class RouteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] Stations { get; set; }
        public string[] TimeOfStopPoints { get; set; }
        public int StartStationId { get; set; }
        public int FinishStationId { get; set; }
        public string TrainName { get; set; }
        public string TrainNumber { get; set; }
        public string StartStation { get; set; }
        public string FinishStation { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int DaysDuration { get; set; }
        public string[] Days { get; set; }
        public string[] StationsNames { get; set; }
        public int[] CarriagesId { get; set; }
        public List<CarriageDTO> Carriages { get; set; }
        public string Duration { get; set; }


    }
}
