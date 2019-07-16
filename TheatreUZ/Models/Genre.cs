using System;
using System.Collections.Generic;

namespace TheatreUZ.Models
{
    public class Genre : BaseModel
    {
        public Guid StateID { get; set; }

        public string Name { get; set; }
        public DateTime RegDate { get; set; }

        public virtual State State { get; set; }

        public ICollection<Spectacle> Spectacles { get; set; }
    }
}