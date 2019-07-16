using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreUZ.Models
{
    public class Role : BaseModel
    {
        public Guid StateID { get; set; }
        public string Name { get; set; }
        public DateTime RegDate { get; set; }

        public virtual State State { get; set; }
        
        public ICollection<User> Users { get; set; }
    }
}