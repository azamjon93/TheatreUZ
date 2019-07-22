﻿using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class UserQueryHandlerFactory
    {
        public static IQueryHandler<AllUsersQuery, IEnumerable<User>> Build(AllUsersQuery query)
        {
            return new AllUsersQueryHandler();
        }

        public static IQueryHandler<OneUserQuery, User> Build(OneUserQuery query)
        {
            return new OneUserQueryHandler(query);
        }
    }

    public class AllUsersQueryHandler : IQueryHandler<AllUsersQuery, IEnumerable<User>>
    {
        public IEnumerable<User> Get()
        {
            var db = new TheatreUZContext();
            return db.Users.OrderBy(w => w.Name);
        }
    }

    public class OneUserQueryHandler : IQueryHandler<OneUserQuery, User>
    {
        private readonly OneUserQuery query;

        public OneUserQueryHandler(OneUserQuery query)
        {
            this.query = query;
        }

        public User Get()
        {
            var db = new TheatreUZContext();
            return db.Users.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}