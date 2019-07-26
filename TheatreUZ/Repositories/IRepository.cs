using System;
using System.Collections.Generic;

namespace TheatreUZ
{
    public interface IRepository
    {
        object GetOne(Guid id);
        IEnumerable<object> GetAll();
        void Save(object obj);
        void Edit(object obj);
        void Delete(Guid id);
    }
}
