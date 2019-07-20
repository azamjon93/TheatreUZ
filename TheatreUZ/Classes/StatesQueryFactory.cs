using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class StateQueryHandlerFactory
    {
        public static IQueryHandler<AllStatesQuery, IEnumerable<State>> Build(AllStatesQuery query)
        {
            return new AllStatesQueryHandler();
        }

        public static IQueryHandler<OneStateQuery, State> Build(OneStateQuery query)
        {
            return new OneStateQueryHandler(query);
        }
    }

    public class AllStatesQueryHandler : IQueryHandler<AllStatesQuery, IEnumerable<State>>
    {
        public IEnumerable<State> Get()
        {
            var db = new TheatreUZContext();
            return db.States.OrderBy(w => w.Name);
        }
    }

    public class OneStateQueryHandler : IQueryHandler<OneStateQuery, State>
    {
        private readonly OneStateQuery query;

        public OneStateQueryHandler(OneStateQuery query)
        {
            this.query = query;
        }

        public State Get()
        {
            var db = new TheatreUZContext();
            return db.States.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}