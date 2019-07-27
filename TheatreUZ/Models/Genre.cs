using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheatreUZ.Models
{
    public class Genre : BaseModel
    {
        public Guid StateID { get; set; }

        [Display(Name = "Genre name")]
        public string Name { get; set; }

        [Display(Name = "Reg. date")]
        public DateTime RegDate { get; set; }
        
        public virtual State State { get; set; }

        public ICollection<Spectacle> Spectacles { get; set; }
    }
}