using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class SaleQueryHandlerFactory
    {
        public static IQueryHandler<AllSalesQuery, IEnumerable<Sale>> Build(AllSalesQuery query, TheatreUZContext dbContext)
        {
            return new AllSalesQueryHandler(dbContext);
        }

        public static IQueryHandler<OneSaleQuery, Sale> Build(OneSaleQuery query, TheatreUZContext dbContext)
        {
            return new OneSaleQueryHandler(query, dbContext);
        }
    }

    public class AllSalesQueryHandler : IQueryHandler<AllSalesQuery, IEnumerable<Sale>>
    {
        TheatreUZContext db;

        public AllSalesQueryHandler(TheatreUZContext dbContext)
        {
            db = dbContext;
        }

        public IEnumerable<Sale> Get()
        {
            return db.Sales.OrderByDescending(w => w.RegDate);
        }
    }

    public class OneSaleQueryHandler : IQueryHandler<OneSaleQuery, Sale>
    {
        private readonly OneSaleQuery query;
        TheatreUZContext db;

        public OneSaleQueryHandler(OneSaleQuery query, TheatreUZContext dbContext)
        {
            this.query = query;
            db = dbContext;
        }

        public Sale Get()
        {
            return db.Sales.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}