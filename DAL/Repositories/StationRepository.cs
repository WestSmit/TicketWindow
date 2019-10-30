using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class StationRepository : IRepository<Station>
    {
        private RouteContext db;

        public StationRepository(RouteContext db)
        {
            this.db = db;
        }
        public void Create(Station item)
        {
            db.Stations.Add(item);
        }

        public void Delete(int id)
        {
            Route route = db.Routes.Find(id);
            if (route != null) db.Routes.Remove(route);
        }

        public IEnumerable<Station> Find(Func<Station, bool> predicate)
        {
            return db.Stations.Where(predicate).ToList();
        }

        public Station Get(int id)
        {
            return db.Stations.Find(id);
        }

        public IEnumerable<Station> GetAll()
        {
            return db.Stations;
        }

        public void Update(Station item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
