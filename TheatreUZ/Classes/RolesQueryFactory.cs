using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class RoleQueryHandlerFactory
    {
        public static IQueryHandler<AllRolesQuery, IEnumerable<Role>> Build(AllRolesQuery query, TheatreUZContext dbContext)
        {
            return new AllRolesQueryHandler(dbContext);
        }

        public static IQueryHandler<OneRoleQuery, Role> Build(OneRoleQuery query, TheatreUZContext dbContext)
        {
            return new OneRoleQueryHandler(query, dbContext);
        }
    }

    public class AllRolesQueryHandler : IQueryHandler<AllRolesQuery, IEnumerable<Role>>
    {
        TheatreUZContext db;
        public AllRolesQueryHandler(TheatreUZContext dbContext)
        {
            db = dbContext;
        }

        public IEnumerable<Role> Get()
        {
            return db.Roles.OrderBy(w => w.Name);
        }
    }

    public class OneRoleQueryHandler : IQueryHandler<OneRoleQuery, Role>
    {
        private readonly OneRoleQuery query;
        TheatreUZContext db;

        public OneRoleQueryHandler(OneRoleQuery query, TheatreUZContext dbContext)
        {
            this.query = query;
            db = dbContext;
        }

        public Role Get()
        {
            return db.Roles.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}