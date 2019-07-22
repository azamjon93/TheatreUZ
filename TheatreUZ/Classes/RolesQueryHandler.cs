using System;
using System.Collections.Generic;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class AllRolesQuery : IQuery<IEnumerable<Role>>
    {

    }

    public class OneRoleQuery : IQuery<Role>
    {
        public Guid ID { get; set; }

        public OneRoleQuery(Guid id)
        {
            ID = id;
        }
    }
}