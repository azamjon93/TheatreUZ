using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheatreUZ.Models
{
    public class Role : BaseModel
    {
        public Guid StateID { get; set; }

        [Display(Name = "Role name")]
        public string Name { get; set; }

        [Display(Name = "Reg. date")]
        public DateTime RegDate { get; set; }

        public virtual State State { get; set; }

        public ICollection<User> Users { get; set; }
    }
}