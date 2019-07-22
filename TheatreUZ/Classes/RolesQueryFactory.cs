using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class RoleQueryHandlerFactory
    {
        public static IQueryHandler<AllRolesQuery, IEnumerable<Role>> Build(AllRolesQuery query)
        {
            return new AllRolesQueryHandler();
        }

        public static IQueryHandler<OneRoleQuery, Role> Build(OneRoleQuery query)
        {
            return new OneRoleQueryHandler(query);
        }
    }

    public class AllRolesQueryHandler : IQueryHandler<AllRolesQuery, IEnumerable<Role>>
    {
        public IEnumerable<Role> Get()
        {
            var db = new TheatreUZContext();
            return db.Roles.OrderBy(w => w.Name);
        }
    }

    public class OneRoleQueryHandler : IQueryHandler<OneRoleQuery, Role>
    {
        private readonly OneRoleQuery query;

        public OneRoleQueryHandler(OneRoleQuery query)
        {
            this.query = query;
        }

        public Role Get()
        {
            var db = new TheatreUZContext();
            return db.Roles.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}