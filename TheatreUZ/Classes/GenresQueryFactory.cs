using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class GenreQueryHandlerFactory
    {
        public static IQueryHandler<AllGenresQuery, IEnumerable<Genre>> Build(AllGenresQuery query, TheatreUZContext dbContext)
        {
            return new AllGenresQueryHandler(dbContext);
        }

        public static IQueryHandler<OneGenreQuery, Genre> Build(OneGenreQuery query, TheatreUZContext dbContext)
        {
            return new OneGenreQueryHandler(query, dbContext);
        }
    }

    public class AllGenresQueryHandler : IQueryHandler<AllGenresQuery, IEnumerable<Genre>>
    {
        TheatreUZContext db;

        public AllGenresQueryHandler(TheatreUZContext dbContext)
        {
            db = dbContext;
        }

        public IEnumerable<Genre> Get()
        {
            return db.Genres.OrderBy(w => w.Name);
        }
    }

    public class OneGenreQueryHandler : IQueryHandler<OneGenreQuery, Genre>
    {
        private readonly OneGenreQuery query;
        TheatreUZContext db;

        public OneGenreQueryHandler(OneGenreQuery query, TheatreUZContext dbContext)
        {
            this.query = query;
            db = dbContext;
        }

        public Genre Get()
        {
            return db.Genres.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}