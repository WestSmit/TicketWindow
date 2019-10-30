using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Entities;
using BLL.Infrastructure;
using AutoMapper;

namespace BLL.Services
{
    public class RouteService : IRouteService
    {
        IUnitOfWork Database { get; set; }
        public RouteService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }
        public void CreateRoute(RouteDTO routeDTO)
        {
            Route NewRoute = new Route
            {
                Id = routeDTO.Id,
                Name = routeDTO.Name,
                Stations = routeDTO.Stations,
                TimeOfStopPoints = routeDTO.TimeOfStopPoints,
                TrainName = routeDTO.TrainName,
                TrainNumber =routeDTO.TrainNumber,
                Days = routeDTO.Days,
                DaysDuration =routeDTO.DaysDuration,
                CarriagesId = routeDTO.CarriagesId,
                FinishTime= routeDTO.FinishTime,
                StartTime = routeDTO.StartTime,           


            };

            Database.Routes.Create(NewRoute);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }


        public RouteDTO GetRoute(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Id is not set", "");
            }
            var Route = Database.Routes.Get(id.Value);
            if (Route == null)
            {
                throw new ValidationException("Route is not found", "");
            }
            var RouteMapper = new MapperConfiguration(cfg => cfg.CreateMap<Route, RouteDTO>()).CreateMapper();
            return RouteMapper.Map<Route, RouteDTO>(Route);
        }

        public IEnumerable<RouteDTO> GetRouteDTOs()
        {
            var RoutesMapper = new MapperConfiguration(cfg => cfg.CreateMap<Route, RouteDTO>()).CreateMapper();
            return RoutesMapper.Map<IEnumerable<Route>, List<RouteDTO>>(Database.Routes.GetAll());
        }

        public List<RouteDTO> SearchRoute(string FromStation, string ToSattion, DateTime Date)
        {
            List<Route> AllRoutes = Database.Routes.GetAll().ToList();

            int FromStationId = Database.Stations.GetAll().Single(x => x.Name == FromStation).Id;
            int ToStationId = Database.Stations.GetAll().Single(x => x.Name == ToSattion).Id;

            //Sort by Names Station
            List<Route> FoundRouters = AllRoutes.FindAll(x => x.Stations.Contains(FromStationId) && x.Stations.Contains(ToStationId));

            foreach (Route route in FoundRouters)
            {
                int IndexA = 0;
                int IndexB = 0;
                for (int i = 0; i < route.Stations.Length; i++)
                {
                    if (route.Stations[i] == FromStationId)
                    {
                        IndexA = i;
                    }
                    else if(route.Stations[i]==ToStationId)
                    {
                        IndexB = i;
                    }
                }
                if (IndexB - IndexA <= 0)
                {
                    FoundRouters.Remove(route);
                }
            }
           
            //Sort by Date
            FoundRouters = FoundRouters.FindAll(x => x.Days.Contains(Date.DayOfWeek.ToString()));
            
            var RoutesMapper = new MapperConfiguration(cfg => cfg.CreateMap<Route, RouteDTO>()).CreateMapper();
            List<RouteDTO> FoundRoutersDTO = RoutesMapper.Map<IEnumerable<Route>, List<RouteDTO>>(FoundRouters);

            foreach (RouteDTO r in FoundRoutersDTO)
            {
                r.StartStationId = FromStationId;
                r.FinishStationId = ToStationId;
                r.StartStation = Database.Stations.Get(r.StartStationId).Name;
                r.FinishStation = Database.Stations.Get(r.FinishStationId).Name;
                r.StationsNames = new string[r.Stations.Length];

                List<Carriage> carriages = Database.Carriages.Find(x => r.CarriagesId.Contains(x.Id)).ToList();

                var CarriageMapper = new MapperConfiguration(cfg => cfg.CreateMap<Carriage, CarriageDTO>()).CreateMapper();
                r.Carriages = CarriageMapper.Map<IEnumerable<Carriage>, List<CarriageDTO>>(carriages);



                for (int i = 0; i < r.Stations.Length; i++)
                {
                    r.StationsNames[i] = Database.Stations.Get(r.Stations[i]).Name;

                    if (r.Stations[i] == r.StartStationId)
                    {
                        r.StartTime = DateTime.Parse(r.TimeOfStopPoints[i]);
                    }
                    else if (r.Stations[i] == r.FinishStationId)
                    {
                        r.FinishTime = DateTime.Parse(r.TimeOfStopPoints[i]);
                    }
                }
                string duration = new TimeSpan(r.FinishTime.AddDays(r.DaysDuration).Ticks - r.StartTime.Ticks).ToString();
                if (duration.Substring(0,1) == "0")
                {
                    duration = duration.Substring(1);
                }
                r.Duration = duration.Split(':')[0] + "h " + duration.Split(':')[1] + "min";
            }
            return FoundRoutersDTO;
        }
    }
}
