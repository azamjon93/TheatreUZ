using System;
using System.ComponentModel.DataAnnotations;

namespace TheatreUZ.Models
{
    public class Notification : BaseModel
    {
        public Guid UserID { get; set; }
        public Guid StateID { get; set; }

        [Display(Name = "Notification message")]
        public string Message { get; set; }

        [Display(Name = "Send date")]
        public DateTime RegDate { get; set; }

        public virtual User User { get; set; }
        public virtual State State { get; set; }

    }
}