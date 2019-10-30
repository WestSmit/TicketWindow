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
        void CreateRoute(RouteDTO routeDTO);
        RouteDTO GetRoute(int? id);
        List<RouteDTO> SearchRoute(string FromStation, string ToSattion, DateTime Date);
        IEnumerable<RouteDTO> GetRouteDTOs();
        void Dispose();
    }
}
