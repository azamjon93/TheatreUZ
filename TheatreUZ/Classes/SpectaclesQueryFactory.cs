using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class SpectacleQueryHandlerFactory
    {
        public static IQueryHandler<AllSpectaclesQuery, IEnumerable<Spectacle>> Build(AllSpectaclesQuery query, TheatreUZContext dbContext)
        {
            return new AllSpectaclesQueryHandler(dbContext);
        }

        public static IQueryHandler<OneSpectacleQuery, Spectacle> Build(OneSpectacleQuery query, TheatreUZContext dbContext)
        {
            return new OneSpectacleQueryHandler(query, dbContext);
        }
    }

    public class AllSpectaclesQueryHandler : IQueryHandler<AllSpectaclesQuery, IEnumerable<Spectacle>>
    {
        TheatreUZContext db;

        public AllSpectaclesQueryHandler(TheatreUZContext dbContext)
        {
            db = dbContext;
        }

        public IEnumerable<Spectacle> Get()
        {
            return db.Spectacles.OrderByDescending(w => w.RegDate);
        }
    }

    public class OneSpectacleQueryHandler : IQueryHandler<OneSpectacleQuery, Spectacle>
    {
        private readonly OneSpectacleQuery query;
        TheatreUZContext db;

        public OneSpectacleQueryHandler(OneSpectacleQuery query, TheatreUZContext dbContext)
        {
            this.query = query;
            db = dbContext;
        }

        public Spectacle Get()
        {
            return db.Spectacles.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}