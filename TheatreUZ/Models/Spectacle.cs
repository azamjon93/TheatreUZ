using System;
using System.Collections.Generic;

namespace TheatreUZ.Models
{
    public class Spectacle : BaseModel
    {
        public Guid GenreID { get; set; }
        public Guid StateID { get; set; }

        public string Name { get; set; }
        public double Cost { get; set; }
        public int TicketsCount { get; set; }
        public DateTime PlayDate { get; set; }
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