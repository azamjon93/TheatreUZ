using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class TheatreUZInitializer : DropCreateDatabaseAlways<TheatreUZContext>
    {
        protected override void Seed(TheatreUZContext db)
        {
            var states = new List<State>
            {
                new State { Name = "Active", RegDate = DateTime.Now },
                new State { Name = "Inactive", RegDate = DateTime.Now },
                new State { Name = "Passed", RegDate = DateTime.Now },
                new State { Name = "Deleted", RegDate = DateTime.Now }
            };

            states.ForEach(s => db.States.Add(s));
            db.SaveChanges();

            var roles = new List<Role>
            {
                new Role { Name = "Admin", RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Role { Name = "User", RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() }
            };

            roles.ForEach(r => db.Roles.Add(r));
            db.SaveChanges();

            var users = new List<User>
            {
                new User { Name = "Administrator of system", Email = "admin@theatre.uz", Role = roles.Where(r => r.Name == "Admin").FirstOrDefault(), State = states.Where(s => s.Name == "Active").FirstOrDefault(), RegDate = DateTime.Now, PasswordHash = "kjfw2837r8" },
                new User { Name = "Azamjon Nabijonov", Email = "mr.nabijonov@gmail.com", Role = roles.Where(r => r.Name == "User").FirstOrDefault(), State = states.Where(s => s.Name == "Active").FirstOrDefault(), RegDate = DateTime.Now, PasswordHash = "flkgjh955" }
            };

            users.ForEach(u => db.Users.Add(u));
            db.SaveChanges();

            var genres = new List<Genre>
            {
                new Genre { Name = "Drama", RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Genre { Name = "Comedy", RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() }
            };

            genres.ForEach(g => db.Genres.Add(g));
            db.SaveChanges();

            var spectacles = new List<Spectacle>
            {
                new Spectacle { Name = "Othello", Cost = 60000, TicketsCount = 100, Genre = genres.Where(g => g.Name == "Drama").FirstOrDefault(), PlayDate = new DateTime(2019, 07, 27, 19, 00, 00), RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Spectacle { Name = "King Leer", Cost = 70000, TicketsCount = 100, Genre = genres.Where(g => g.Name == "Drama").FirstOrDefault(), PlayDate = new DateTime(2019, 07, 28, 19, 00, 00), RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Spectacle { Name = "May I come in?", Cost = 80000, TicketsCount = 100, Genre = genres.Where(g => g.Name == "Comedy").FirstOrDefault(), PlayDate = new DateTime(2019, 07, 29, 19, 00, 00), RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Spectacle { Name = "Colorful life", Cost = 90000, TicketsCount = 100, Genre = genres.Where(g => g.Name == "Comedy").FirstOrDefault(), PlayDate = new DateTime(2019, 07, 30, 19, 00, 00), RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() }
            };

            spectacles.ForEach(s => db.Spectacles.Add(s));
            db.SaveChanges();

            var sales = new List<Sale>
            {
                new Sale { User = users.Where(u => u.Email == "mr.nabijonov@gmail.com").FirstOrDefault(), Spectacle = spectacles.Where(s => s.Name == "Othello").FirstOrDefault(), Amount = 2, RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Sale { User = users.Where(u => u.Email == "mr.nabijonov@gmail.com").FirstOrDefault(), Spectacle = spectacles.Where(s => s.Name == "King Leer").FirstOrDefault(), Amount = 1, RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() }
            };

            sales.ForEach(s => db.Sales.Add(s));
            db.SaveChanges();

            var notifications = new List<Notification>
            {
                new Notification { User = users.Where(u => u.Email == "mr.nabijonov@gmail.com").FirstOrDefault(), Message = "Hello", RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Notification { User = users.Where(u => u.Email == "mr.nabijonov@gmail.com").FirstOrDefault(), Message = "Thanks", RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() }
            };

            notifications.ForEach(n => db.Notifications.Add(n));
            db.SaveChanges();

            base.Seed(db);
        }
    }
}