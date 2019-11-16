using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;

namespace DAL.Repositories
{
    public class TicketRepository : IRepository<Ticket>
    {
        private RouteContext db;

        public TicketRepository(RouteContext db)
        {
            this.db = db;
        }
        public void Create(Ticket item)
        {
            db.Tickets.Add(item);
        }

        public void Delete(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket != null) db.Tickets.Remove(ticket);
        }

        public IEnumerable<Ticket> Find(Func<Ticket, bool> predicate)
        {
            return db.Tickets.Where(predicate).ToList();
        }

        public Ticket Get(int id)
        {
            return db.Tickets.Find(id);
        }

        public IEnumerable<Ticket> GetAll()
        {
            return db.Tickets;
        }

        public void Update(Ticket item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
