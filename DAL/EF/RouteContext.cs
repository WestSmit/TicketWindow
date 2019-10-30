using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL.EF
{
    public class RouteContext : DbContext
    {
        public DbSet<Route> Routes { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Carriage> Carriages { get; set; }
        static RouteContext()
        {
            Database.SetInitializer<RouteContext>(new DbInitializer());
        }
        public RouteContext(string connectionString) : base(connectionString)
        {

        }

    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<RouteContext>
    {
        protected override void Seed(RouteContext db)
        {
          
            db.Stations.Add(new Station { Id = 1, Name = "Kyiv" });
            db.Stations.Add(new Station { Id = 2, Name = "Kharkiv" });
            db.Stations.Add(new Station { Id = 3, Name = "Myrhorod" });
            db.Stations.Add(new Station { Id = 4, Name = "Poltava" });
            db.Carriages.Add(new Carriage { Id = 1, FreePlaces = 40, NumberOfPlaces = 60, Type= "2nd class sleeper" });
            db.Carriages.Add(new Carriage { Id = 2, FreePlaces = 55, NumberOfPlaces = 60, Type = "1st class sleeper" });
            db.Carriages.Add(new Carriage { Id = 3, FreePlaces = 50, NumberOfPlaces = 60, Type = "3rd class sleeper" });
            db.Carriages.Add(new Carriage { Id = 4, FreePlaces = 43, NumberOfPlaces = 60, Type = "Seat:Standart" });
            db.Carriages.Add(new Carriage { Id = 5, FreePlaces = 12, NumberOfPlaces = 60, Type = "Seat:Economic" });
            db.Routes.Add(new Route
            {
                Id = 1,
                Name = "Kyiv - Kharkiv",
                Days = new string[] { DayOfWeek.Monday.ToString(), DayOfWeek.Friday.ToString() },
                DaysDuration = 0,
                StartTime = new DateTime(2019, 1, 1, 7, 20, 1),
                FinishTime = new DateTime(2019, 1, 1, 14, 40, 1),
                Stations = new int[] { 1, 3, 4, 2 },
                TimeOfStopPoints = new string[]
                {
                    new DateTime(2019, 1, 1, 12, 30, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 16, 35, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 17, 40, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 18, 40, 1).ToString("HH:mm")
                },
                CarriagesId = new int[] {1,2,3}
            });


            db.Routes.Add(new Route
            {
                Id = 2,
                Name = "Kyiv - Kharkiv (new)",
                Days = new string[] { DayOfWeek.Monday.ToString(), DayOfWeek.Tuesday.ToString() },
                DaysDuration = 0,
                StartTime = new DateTime(2019, 1, 1, 15, 30, 1),
                FinishTime = new DateTime(2019, 1, 1, 20, 50, 1),
                Stations = new int[] { 1, 3, 4, 2 },
                TimeOfStopPoints = new string[]
                {
                    new DateTime(2019, 1, 1, 12, 30, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 16, 35, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 17, 40, 1).ToString("HH:mm"),
                    new DateTime(2019, 1, 1, 18, 40, 1).ToString("HH:mm")
                },
                CarriagesId= new int[] {4,5}
            });

            db.SaveChanges();
        }
    }
}
