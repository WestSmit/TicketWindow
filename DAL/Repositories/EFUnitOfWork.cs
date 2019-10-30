using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private RouteContext db;
        private RouteRepository RouteRepository;
        private StationRepository StationRepository;
        private CarriageRepository CarriageRepository;
        public EFUnitOfWork(string connectionString)
        {
            db = new RouteContext(connectionString);
        }

        public IRepository<Route> Routes
        {
            get
            {
                if (RouteRepository == null)
                {
                    RouteRepository = new RouteRepository(db);
                }
                return RouteRepository;
            }
        }

        public IRepository<Station> Stations
        {
            get
            {
                if (StationRepository == null)
                {
                    StationRepository = new StationRepository(db);
                }
                return StationRepository;
            }
        }
        public IRepository<Carriage> Carriages
        {
            get
            {
                if (CarriageRepository == null)
                {
                    CarriageRepository = new CarriageRepository(db);
                }
                return CarriageRepository;
            }
        }


        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                    this.disposed = true;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
