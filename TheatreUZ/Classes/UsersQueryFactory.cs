using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class UserQueryHandlerFactory
    {
        public static IQueryHandler<AllUsersQuery, IEnumerable<User>> Build(AllUsersQuery query, TheatreUZContext dbContext)
        {
            return new AllUsersQueryHandler(dbContext);
        }

        public static IQueryHandler<OneUserQuery, User> Build(OneUserQuery query, TheatreUZContext dbContext)
        {
            return new OneUserQueryHandler(query, dbContext);
        }

        public static IQueryHandler<OneUserByEmailQuery, User> Build(OneUserByEmailQuery query, TheatreUZContext dbContext)
        {
            return new OneUserByEmailQueryHandler(query, dbContext);
        }
    }

    public class AllUsersQueryHandler : IQueryHandler<AllUsersQuery, IEnumerable<User>>
    {
        TheatreUZContext db;

        public AllUsersQueryHandler(TheatreUZContext dbContext)
        {
            db = dbContext;
        }

        public IEnumerable<User> Get()
        {
            return db.Users.OrderBy(w => w.Name);
        }
    }

    public class OneUserQueryHandler : IQueryHandler<OneUserQuery, User>
    {
        private readonly OneUserQuery query;
        TheatreUZContext db;

        public OneUserQueryHandler(OneUserQuery query, TheatreUZContext dbContext)
        {
            this.query = query;
            db = dbContext;
        }

        public User Get()
        {
            return db.Users.FirstOrDefault(s => s.ID == query.ID);
        }
    }
    
    public class OneUserByEmailQueryHandler : IQueryHandler<OneUserByEmailQuery, User>
    {
        private readonly OneUserByEmailQuery query;
        TheatreUZContext db;

        public OneUserByEmailQueryHandler(OneUserByEmailQuery query, TheatreUZContext dbContext)
        {
            this.query = query;
            db = dbContext;
        }

        public User Get()
        {
            return db.Users.FirstOrDefault(s => s.Email == query.Email);
        }
    }
}