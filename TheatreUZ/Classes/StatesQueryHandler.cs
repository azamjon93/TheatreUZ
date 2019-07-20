using System;
using System.Collections.Generic;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class AllStatesQuery : IQuery<IEnumerable<State>>
    {

    }

    public class OneStateQuery : IQuery<State>
    {
        public Guid ID { get; set; }

        public OneStateQuery(Guid id)
        {
            ID = id;
        }
    }
}