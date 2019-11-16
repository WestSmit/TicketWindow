using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IRouteService
    {
        void AddRoute(RouteDTO routeDTO, string[] CarriagesTypes);
        RouteDTO GetRoute(int? id);
        List<RouteDTO> SearchRoute(string FromStation, string ToSattion, DateTime Date);
        IEnumerable<RouteDTO> GetRouteDTOs();
        void AddStation(StationDTO stationDTO);
        List<StationDTO> GetStations();
        void ToFreeSeat(int RouteId, int WagonNumber);
        int GetSeat(int RouteId, int WagonNumber);
        TicketDTO BuyTicket(int RouteId, int WagonNumber, int SeatNumber);
        List<TicketDTO> GetTickets();
        void Dispose();
    }
}
