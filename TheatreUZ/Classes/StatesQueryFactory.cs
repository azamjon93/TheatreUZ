using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class StateQueryHandlerFactory
    {
        public static IQueryHandler<AllStatesQuery, IEnumerable<State>> Build(AllStatesQuery query, TheatreUZContext dbContext)
        {
            return new AllStatesQueryHandler(dbContext);
        }

        public static IQueryHandler<OneStateQuery, State> Build(OneStateQuery query, TheatreUZContext dbContext)
        {
            return new OneStateQueryHandler(query, dbContext);
        }
    }

    public class AllStatesQueryHandler : IQueryHandler<AllStatesQuery, IEnumerable<State>>
    {
        TheatreUZContext db;

        public AllStatesQueryHandler(TheatreUZContext dbContext)
        {
            db = dbContext;
        }

        public IEnumerable<State> Get()
        {
            return db.States.OrderBy(w => w.Name);
        }
    }

    public class OneStateQueryHandler : IQueryHandler<OneStateQuery, State>
    {
        private readonly OneStateQuery query;
        TheatreUZContext db;

        public OneStateQueryHandler(OneStateQuery query, TheatreUZContext dbContext)
        {
            this.query = query;
            db = dbContext;
        }

        public State Get()
        {
            return db.States.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}