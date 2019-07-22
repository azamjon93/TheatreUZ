using System;
using System.Collections.Generic;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class AllGenresQuery : IQuery<IEnumerable<Genre>>
    {

    }

    public class OneGenreQuery : IQuery<Genre>
    {
        public Guid ID { get; set; }

        public OneGenreQuery(Guid id)
        {
            ID = id;
        }
    }
}