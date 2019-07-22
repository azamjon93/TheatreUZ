using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class GenreQueryHandlerFactory
    {
        public static IQueryHandler<AllGenresQuery, IEnumerable<Genre>> Build(AllGenresQuery query)
        {
            return new AllGenresQueryHandler();
        }

        public static IQueryHandler<OneGenreQuery, Genre> Build(OneGenreQuery query)
        {
            return new OneGenreQueryHandler(query);
        }
    }

    public class AllGenresQueryHandler : IQueryHandler<AllGenresQuery, IEnumerable<Genre>>
    {
        public IEnumerable<Genre> Get()
        {
            var db = new TheatreUZContext();
            return db.Genres.OrderBy(w => w.Name);
        }
    }

    public class OneGenreQueryHandler : IQueryHandler<OneGenreQuery, Genre>
    {
        private readonly OneGenreQuery query;

        public OneGenreQueryHandler(OneGenreQuery query)
        {
            this.query = query;
        }

        public Genre Get()
        {
            var db = new TheatreUZContext();
            return db.Genres.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}