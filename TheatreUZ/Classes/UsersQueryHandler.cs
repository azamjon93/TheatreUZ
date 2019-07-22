﻿using System;
using System.Collections.Generic;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class AllUsersQuery : IQuery<IEnumerable<User>>
    {

    }

    public class OneUserQuery : IQuery<User>
    {
        public Guid ID { get; set; }

        public OneUserQuery(Guid id)
        {
            ID = id;
        }
    }
}