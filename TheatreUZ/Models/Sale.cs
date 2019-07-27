using System;
using System.ComponentModel.DataAnnotations;

namespace TheatreUZ.Models
{
    public class Sale : BaseModel
    {
        public Guid UserID { get; set; }
        public Guid SpectacleID { get; set; }
        public Guid StateID { get; set; }
        
        public int Amount { get; set; }

        [Display(Name = "Booked date")]
        public DateTime RegDate { get; set; }

        public virtual User User { get; set; }
        public virtual Spectacle Spectacle { get; set; }
        public virtual State State { get; set; }

    }
}