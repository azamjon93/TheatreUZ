using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class TheatreUZInitializer : DropCreateDatabaseAlways<TheatreUZContext>
    {
        protected override void Seed(TheatreUZContext context)
        {
            var states = new List<State>
            {
                new State { Name = "Active" },
                new State { Name = "Inactive" },
                new State { Name = "Passed" },
                new State { Name = "Deleted" }
            };

            states.ForEach(s => context.States.Add(s));
            context.SaveChanges();

            var roles = new List<Role>
            {
                new Role { Name = "Admin", RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Role { Name = "User", RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() }
            };

            roles.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();

            var users = new List<User>
            {
                new User { Name = "Administrator of system", Email = "admin@theatre.uz", Role = roles.Where(r => r.Name == "Admin").FirstOrDefault(), State = states.Where(s => s.Name == "Active").FirstOrDefault(), RegDate = DateTime.Now, PasswordHash = "kjfw2837r8" },
                new User { Name = "Azamjon Nabijonov", Email = "mr.nabijonov@gmail.com", Role = roles.Where(r => r.Name == "User").FirstOrDefault(), State = states.Where(s => s.Name == "Active").FirstOrDefault(), RegDate = DateTime.Now, PasswordHash = "flkgjh955" }
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var genres = new List<Genre>
            {
                new Genre { Name = "Drama", RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Genre { Name = "Comedy", RegDate = DateTime.Now, State = states.Where(s => s.Name == "Active").FirstOrDefault() }
            };

            genres.ForEach(g => context.Genres.Add(g));
            context.SaveChanges();

            var spectacles = new List<Spectacle>
            {
                new Spectacle { Name = "Othello", Cost = 60000, Genre = genres.Where(g => g.Name == "Drama").FirstOrDefault(), PlayDate = new DateTime(2019, 07, 27, 19, 00, 00), RegDate = DateTime.Now, State= states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Spectacle { Name = "King Leer", Cost = 70000, Genre = genres.Where(g => g.Name == "Drama").FirstOrDefault(), PlayDate = new DateTime(2019, 07, 28, 19, 00, 00), RegDate = DateTime.Now, State= states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Spectacle { Name = "May I come in?", Cost = 80000, Genre = genres.Where(g => g.Name == "Comedy").FirstOrDefault(), PlayDate = new DateTime(2019, 07, 29, 19, 00, 00), RegDate = DateTime.Now, State= states.Where(s => s.Name == "Active").FirstOrDefault() },
                new Spectacle { Name = "Colorful life", Cost = 90000, Genre = genres.Where(g => g.Name == "Comedy").FirstOrDefault(), PlayDate = new DateTime(2019, 07, 30, 19, 00, 00), RegDate = DateTime.Now, State= states.Where(s => s.Name == "Active").FirstOrDefault() }
            };

            spectacles.ForEach(s => context.Spectacles.Add(s));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}