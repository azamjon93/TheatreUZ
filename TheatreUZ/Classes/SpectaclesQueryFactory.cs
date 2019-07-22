using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class SpectacleQueryHandlerFactory
    {
        public static IQueryHandler<AllSpectaclesQuery, IEnumerable<Spectacle>> Build(AllSpectaclesQuery query)
        {
            return new AllSpectaclesQueryHandler();
        }

        public static IQueryHandler<OneSpectacleQuery, Spectacle> Build(OneSpectacleQuery query)
        {
            return new OneSpectacleQueryHandler(query);
        }
    }

    public class AllSpectaclesQueryHandler : IQueryHandler<AllSpectaclesQuery, IEnumerable<Spectacle>>
    {
        public IEnumerable<Spectacle> Get()
        {
            var db = new TheatreUZContext();
            return db.Spectacles.OrderByDescending(w => w.RegDate);
        }
    }

    public class OneSpectacleQueryHandler : IQueryHandler<OneSpectacleQuery, Spectacle>
    {
        private readonly OneSpectacleQuery query;

        public OneSpectacleQueryHandler(OneSpectacleQuery query)
        {
            this.query = query;
        }

        public Spectacle Get()
        {
            var db = new TheatreUZContext();
            return db.Spectacles.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}