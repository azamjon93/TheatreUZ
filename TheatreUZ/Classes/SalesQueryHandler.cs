using System;
using System.Collections.Generic;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class AllSalesQuery : IQuery<IEnumerable<Sale>>
    {

    }

    public class OneSaleQuery : IQuery<Sale>
    {
        public Guid ID { get; set; }

        public OneSaleQuery(Guid id)
        {
            ID = id;
        }
    }
}