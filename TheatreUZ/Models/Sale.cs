using System;

namespace TheatreUZ.Models
{
    public class Sale : BaseModel
    {
        public Guid UserID { get; set; }
        public Guid SpectacleID { get; set; }
        public Guid StateID { get; set; }

        public int Amount { get; set; }
        public DateTime RegDate { get; set; }

        public virtual User User { get; set; }
        public virtual Spectacle Spectacle { get; set; }
        public virtual State State { get; set; }

    }
}