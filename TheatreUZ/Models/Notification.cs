using System;

namespace TheatreUZ.Models
{
    public class Notification : BaseModel
    {
        public Guid UserID { get; set; }
        public Guid StateID { get; set; }

        public string Message { get; set; }
        public DateTime RegDate { get; set; }

        public virtual User User { get; set; }
        public virtual State State { get; set; }

    }
}