using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheatreUZ.Models
{
    public class Spectacle : BaseModel
    {
        public Guid GenreID { get; set; }
        public Guid StateID { get; set; }

        [Display(Name = "Spectacle name")]
        public string Name { get; set; }

        [Display(Name = "Cost of ticket")]
        public double Cost { get; set; }

        [Display(Name = "Tickets count")]
        public int TicketsCount { get; set; }

        [Display(Name = "Play date")]
        public DateTime PlayDate { get; set; }

        [Display(Name = "Reg. date")]
        public DateTime RegDate { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual State State { get; set; }

        public ICollection<Sale> Sales { get; set; }

    }

    public class SpectaclePageModel
    {
        public PageInfo PageInfo { get; set; }

        public List<Spectacle> Spectacles { get; set; }
    }

    public class SpectacleReadModel
    {
        public Spectacle Spectacle { get; set; }

        public string Image { get; set; }
        
        public int Remain { get; set; }
    }
}