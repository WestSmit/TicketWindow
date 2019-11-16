using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        IRepository<Route> Routes { get; }
        IRepository<Station> Stations { get; }
        IRepository<Carriage> Carriages { get; }
        IRepository<Ticket> Tickets { get; }
        void Save();


    }
}
