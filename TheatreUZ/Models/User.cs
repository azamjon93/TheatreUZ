using System;
using System.Collections.Generic;

namespace TheatreUZ.Models
{
    public class User : BaseModel
    {
        public Guid RoleID { get; set; }
        public Guid StateID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegDate { get; set; }

        public virtual Role Role { get; set; }
        public virtual State State { get; set; }

        public ICollection<Sale> Sales { get; set; }
        public ICollection<Notification> Notifications { get; set; }

    }
}