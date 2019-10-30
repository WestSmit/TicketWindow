using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class RouteRepository : IRepository<Route>
    {
        private RouteContext db;

        public RouteRepository(RouteContext db)
        {
            this.db = db;
        }
        public void Create(Route item)
        {
            db.Routes.Add(item);
        }

        public void Delete(int id)
        {
            Route route = db.Routes.Find(id);
            if (route != null) db.Routes.Remove(route);
        }

        public IEnumerable<Route> Find(Func<Route, bool> predicate)
        {
            return db.Routes.Where(predicate).ToList();
        }

        public Route Get(int id)
        {
            return db.Routes.Find(id);
        }

        public IEnumerable<Route> GetAll()
        {
            return db.Routes;
        }

        public void Update(Route item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
