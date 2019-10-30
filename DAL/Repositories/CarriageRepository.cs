using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.EF;
using DAL.Entities;

namespace DAL.Repositories
{
    public class CarriageRepository : IRepository<Carriage>
    {
        
            private RouteContext db;

            public CarriageRepository(RouteContext db)
            {
                this.db = db;
            }
            public void Create(Carriage item)
            {
                db.Carriages.Add(item);
            }

            public void Delete(int id)
            {
                 Carriage carriage = db.Carriages.Find(id);
                if (carriage != null) db.Carriages.Remove(carriage);
            }

            public IEnumerable<Carriage> Find(Func<Carriage, bool> predicate)
            {
                return db.Carriages.Where(predicate).ToList();
            }

            public Carriage Get(int id)
            {
                return db.Carriages.Find(id);
            }

            public IEnumerable<Carriage> GetAll()
            {
                return db.Carriages;
            }

            public void Update(Carriage item)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            }
        
    }
}
