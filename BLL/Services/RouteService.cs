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
            var RouteDTO = RouteMapper.Map<Route, RouteDTO>(Route);

            RouteDTO.StationsNames = new string[RouteDTO.Stations.Length];

            List<Carriage> carriages = Database.Carriages.Find(x => RouteDTO.CarriagesId.Contains(x.Id)).ToList();

            var CarriageMapper = new MapperConfiguration(cfg => cfg.CreateMap<Carriage, CarriageDTO>()).CreateMapper();
            RouteDTO.Carriages = CarriageMapper.Map<IEnumerable<Carriage>, List<CarriageDTO>>(carriages);

            for (int i = 0; i < RouteDTO.Stations.Length; i++)
            {
                RouteDTO.StationsNames[i] = Database.Stations.Get(RouteDTO.Stations[i]).Name;

                if (RouteDTO.Stations[i] == RouteDTO.StartStationId)
                {
                    RouteDTO.StartTime = DateTime.Parse(RouteDTO.TimeOfStopPoints[i]);
                }
                else if (RouteDTO.Stations[i] == RouteDTO.FinishStationId)
                {
                    RouteDTO.FinishTime = DateTime.Parse(RouteDTO.TimeOfStopPoints[i]);
                }
            }
            string duration = new TimeSpan(RouteDTO.FinishTime.AddDays(RouteDTO.DaysDuration).Ticks - RouteDTO.StartTime.Ticks).ToString();
            if (duration.Substring(0, 1) == "0")
            {
                duration = duration.Substring(1);
            }
            RouteDTO.Duration = duration.Split(':')[0] + "h " + duration.Split(':')[1] + "min";

            return RouteDTO;
        }

        public IEnumerable<RouteDTO> GetRouteDTOs()
        {
            var RoutesMapper = new MapperConfiguration(cfg => cfg.CreateMap<Route, RouteDTO>()).CreateMapper();
            return RoutesMapper.Map<IEnumerable<Route>, List<RouteDTO>>(Database.Routes.GetAll());
        }

        public List<RouteDTO> SearchRoute(string FromStation, string ToSattion, DateTime Date)
        {
            if (FromStation == ToSattion)
            {
                throw new ValidationException("Place of departure and place of arrival may not be the same.", "");
            }
            List<Route> AllRoutes = Database.Routes.GetAll().ToList();

            int FromStationId = Database.Stations.GetAll().Single(x => x.Name == FromStation).Id;
            int ToStationId = Database.Stations.GetAll().Single(x => x.Name == ToSattion).Id;

            //Sort by Names Station
            List<Route> FoundRouters = AllRoutes.FindAll(x => x.Stations.Contains(FromStationId) && x.Stations.Contains(ToStationId));

            //Stantion drectional check
            List<Route> FiltredRouters = new List<Route>();
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
                    else if (route.Stations[i] == ToStationId)
                    {
                        IndexB = i;
                    }
                }
                if (IndexB - IndexA <= 0)
                {

                }
                else
                {
                    FiltredRouters.Add(route);
                }
            }

            //Sort by Date
            FiltredRouters = FiltredRouters.FindAll(x => x.Days.Contains(Date.DayOfWeek.ToString()));

            var RoutesMapper = new MapperConfiguration(cfg => cfg.CreateMap<Route, RouteDTO>()).CreateMapper();
            List<RouteDTO> FoundRoutersDTO = RoutesMapper.Map<IEnumerable<Route>, List<RouteDTO>>(FiltredRouters);

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

                int indexStart = 0;
                int indexFinish = 0;
                for (int i = 0; i < r.Stations.Length; i++)
                {
                    r.StationsNames[i] = Database.Stations.Get(r.Stations[i]).Name;

                    if (r.Stations[i] == r.StartStationId)
                    {
                        r.StartTime = DateTime.Parse(r.TimeOfStopPoints[i]);
                        indexStart = i;
                    }
                    else if (r.Stations[i] == r.FinishStationId)
                    {
                        r.FinishTime = DateTime.Parse(r.TimeOfStopPoints[i]);
                        indexFinish = i;
                    }
                }

                int DaysCounter = 0;
                for (int i = indexStart; i < indexFinish; i++)
                {
                    int res = DateTime.Compare(DateTime.Parse(r.TimeOfStopPoints[i]), DateTime.Parse(r.TimeOfStopPoints[i + 1]));
                    if (res > 0)
                    {
                        DaysCounter++;
                    }
                }

                string duration = new TimeSpan(r.FinishTime.AddDays(DaysCounter).Ticks - r.StartTime.Ticks).ToString();
                if (duration.Substring(0, 1) == "0")
                {
                    duration = duration.Substring(1);
                }
                if (duration.Split(new char[] { ':', '.' }).Length > 3)
                {
                    r.Duration = duration.Split(new char[] { ':', '.' })[0] + "d " + duration.Split(new char[] { ':', '.' })[1] + "h " + duration.Split(new char[] { ':', '.' })[2] + "min";
                }
                else
                {
                    r.Duration = duration.Split(new char[] { ':', '.' })[0] + "h " + duration.Split(new char[] { ':', '.' })[1] + "min";
                }
            }
            return FoundRoutersDTO;
        }
        public void AddStation(StationDTO stationDTO)
        {
            List<Station> Stations = Database.Stations.GetAll().Where(x => x.Name == stationDTO.Name).ToList();

            if (Stations.Count == 0)
            {
                Station station = new Station
                {
                    Name = stationDTO.Name,
                    Id = Database.Stations.GetAll().Last().Id + 1
                };

                Database.Stations.Create(station);
                Database.Save();
            }
            else
            {
                throw new ValidationException("Station already added!", "");
            }
        }
        public List<StationDTO> GetStations()
        {
            var StationMapper = new MapperConfiguration(cfg => cfg.CreateMap<Station, StationDTO>()).CreateMapper();
            return StationMapper.Map<IEnumerable<Station>, List<StationDTO>>(Database.Stations.GetAll());

        }

        public void AddRoute(RouteDTO routeDTO, string[] CarriagesTypes)
        {

            if(routeDTO.Days.Length == 0)
            {
                throw new ValidationException("Route days is not selected", "");
            }

            int NewRouteId = Database.Routes.GetAll().Last().Id + 1;
            int[] carriages = new int[CarriagesTypes.Length];
            for (int i = 0; i < CarriagesTypes.Length; i++)
            {
                int id = Database.Carriages.GetAll().Last().Id + 1 + i;
                Carriage NewCarriage = new Carriage()
                {
                    Id = id,
                    FreePlaces = 60,
                    NumberOfPlaces = 60,
                    RouteId = NewRouteId,
                    Type = CarriagesTypes[i],
                    Number = i + 1
                };
                Database.Carriages.Create(NewCarriage);

                carriages[i] = id;
            }

            int DaysCounter = 0;
            for (int i = 0; i < routeDTO.TimeOfStopPoints.Length - 1; i++)
            {
                int res = DateTime.Compare(DateTime.Parse(routeDTO.TimeOfStopPoints[i]), DateTime.Parse(routeDTO.TimeOfStopPoints[i + 1]));

                if (res > 0)
                {
                    DaysCounter++;
                }
            }

            //string Stations to int StationsId
            int[] StationsId = new int[routeDTO.StationsNames.Length];
            for (int i = 0; i < routeDTO.StationsNames.Length; i++)
            {
                int id = Database.Stations.GetAll().Single(x => (x.Name == routeDTO.StationsNames[i])).Id;
                StationsId[i] = id;
            }

            Route NewRoute = new Route()
            {
                Id = NewRouteId,
                Name = routeDTO.Name,
                TrainName = routeDTO.TrainName,
                TrainNumber = routeDTO.TrainNumber,
                CarriagesId = carriages,
                DaysDuration = DaysCounter,
                Stations = StationsId,
                TimeOfStopPoints = routeDTO.TimeOfStopPoints,
                Days = routeDTO.Days,
            };
            Database.Routes.Create(NewRoute);
            Database.Save();
        }

        public TicketDTO BuyTicket(int RouteId, int WagonNumber, int SeatNumber)
        {
            var route = GetRoute(RouteId);

            var carriage = Database.Carriages.Get(route.Carriages.Single(x => x.Number == WagonNumber).Id);
            Database.Carriages.Update(carriage);
            Ticket ticket = new Ticket
            {
                Id = Database.Tickets.GetAll().Count(),
                Route = route.Id,
                SeatNumber = SeatNumber,
                WagonNumber = WagonNumber
            };

            Database.Tickets.Create(ticket);
            Database.Save();

            TicketDTO ticketDTO = new TicketDTO
            {
                Id = ticket.Id,
                Route = route,
                Wagon = route.Carriages.Single(x => x.Number == WagonNumber),
                SeatNumber = SeatNumber
            };
            return ticketDTO;
        }
        public int GetSeat(int RouteId, int WagonNumber)
        {
            var route = GetRoute(RouteId);

            var carriage = Database.Carriages.Get(route.Carriages.Single(x => x.Number == WagonNumber).Id);
            carriage.FreePlaces--;
            Database.Carriages.Update(carriage);
            Database.Save();
            return carriage.NumberOfPlaces - carriage.FreePlaces;
        }
        public void ToFreeSeat(int RouteId, int WagonNumber)
        {
            var route = GetRoute(RouteId);

            var carriage = Database.Carriages.Get(route.Carriages.Single(x => x.Number == WagonNumber).Id);
            carriage.FreePlaces++;
            Database.Carriages.Update(carriage);
            Database.Save();
        }
        public List<TicketDTO> GetTickets()
        {
            List<Ticket> tickets = Database.Tickets.GetAll().ToList();
            List<TicketDTO> ticketDTOs = new List<TicketDTO>();

            foreach (var t in tickets)
            {
                RouteDTO routeDTO = GetRoute(t.Route);

                Carriage carriage = Database.Carriages.GetAll().Single(x => x.Number == t.WagonNumber && x.RouteId == t.Route);
                var CarriageMapper = new MapperConfiguration(cfg => cfg.CreateMap<Carriage, CarriageDTO>()).CreateMapper();

                CarriageDTO CarriageDTO = CarriageMapper.Map<Carriage, CarriageDTO>(carriage);

                TicketDTO ticketDTO = new TicketDTO
                {
                    Id = t.Id,
                    Route = routeDTO,
                    Wagon = CarriageDTO,
                    SeatNumber = t.SeatNumber
                };
                ticketDTOs.Add(ticketDTO);
            }
            return ticketDTOs;

        }
    }
}
