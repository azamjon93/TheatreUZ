using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheatreUZ.Models
{
    public class User : BaseModel
    {
        public Guid RoleID { get; set; }
        public Guid StateID { get; set; }

        [Display(Name = "User's full name")]
        public string Name { get; set; }

        [Display(Name = "E-Mail address")]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        [Display(Name = "Reg. date")]
        public DateTime RegDate { get; set; }

        public virtual Role Role { get; set; }
        public virtual State State { get; set; }

        public ICollection<Sale> Sales { get; set; }
        public ICollection<Notification> Notifications { get; set; }

    }

    public class UserAllInfoModel
    {
        public User User { get; set; }
        public List<Sale> Sales { get; set; }

    }
}