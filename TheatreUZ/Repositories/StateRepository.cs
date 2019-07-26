using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class StateRepository : IRepository
    {
        TheatreUZContext db = new TheatreUZContext();

        public object GetOne(Guid id)
        {
            return StateQueryHandlerFactory.Build(new OneStateQuery(id)).Get();
        }

        public IEnumerable<object> GetAll()
        {
            var x = new AllStatesQueryHandler();
            return x.Get();
        }

        public void Save(object obj)
        {
            State s = (State)obj;
            var x = StateSaveCommandHandlerFactory.Build(new StateSaveCommand(s));
            x.Execute();
        }

        public void Edit(object obj)
        {
            State s = (State)obj;
            var x = StateSaveCommandHandlerFactory.Build(new StateSaveCommand(s));
            x.Execute();
        }

        public void Delete(Guid id)
        {
            var x = StateDeleteCommandHandlerFactory.Build(new StateDeleteCommand(id));
            x.Execute();
        }
    }
}