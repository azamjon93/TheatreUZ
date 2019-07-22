using System;
using System.Collections.Generic;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class AllSpectaclesQuery : IQuery<IEnumerable<Spectacle>>
    {

    }

    public class OneSpectacleQuery : IQuery<Spectacle>
    {
        public Guid ID { get; set; }

        public OneSpectacleQuery(Guid id)
        {
            ID = id;
        }
    }
}