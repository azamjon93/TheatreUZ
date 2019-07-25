using System;
using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class SaleQueryHandlerFactory
    {
        public static IQueryHandler<AllSalesQuery, IEnumerable<Sale>> Build(AllSalesQuery query)
        {
            return new AllSalesQueryHandler();
        }
        
        public static IQueryHandler<OneSaleQuery, Sale> Build(OneSaleQuery query)
        {
            return new OneSaleQueryHandler(query);
        }
    }

    public class AllSalesQueryHandler : IQueryHandler<AllSalesQuery, IEnumerable<Sale>>
    {
        public IEnumerable<Sale> Get()
        {
            var db = new TheatreUZContext();
            return db.Sales.OrderByDescending(w => w.RegDate);
        }
        
    }
    
    public class OneSaleQueryHandler : IQueryHandler<OneSaleQuery, Sale>
    {
        private readonly OneSaleQuery query;

        public OneSaleQueryHandler(OneSaleQuery query)
        {
            this.query = query;
        }

        public Sale Get()
        {
            var db = new TheatreUZContext();
            return db.Sales.FirstOrDefault(s => s.ID == query.ID);
        }
        
    }
}