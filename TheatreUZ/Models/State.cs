using System;
using System.Collections.Generic;

namespace TheatreUZ.Models
{
    public class State : BaseModel
    {
        public string Name { get; set; }
        public DateTime RegDate { get; set; }

        public ICollection<Role> Roles { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Spectacle> Spectacles { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Notification> Notifications { get; set; }

    }
}