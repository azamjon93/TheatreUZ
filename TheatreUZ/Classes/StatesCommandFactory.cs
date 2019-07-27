using System;
using System.Linq;

namespace TheatreUZ
{
    public static class StateSaveCommandHandlerFactory
    {
        public static ICommandHandler<StateSaveCommand, CommandResponse> Build(StateSaveCommand command, TheatreUZContext dbContext)
        {
            return new StateSaveCommandHandler(command, dbContext);
        }
    }

    public static class StateDeleteCommandHandlerFactory
    {
        public static ICommandHandler<StateDeleteCommand, CommandResponse> Build(StateDeleteCommand command, TheatreUZContext dbContext)
        {
            return new StateDeleteCommandHandler(command, dbContext);
        }
    }

    public class StateSaveCommandHandler : ICommandHandler<StateSaveCommand, CommandResponse>
    {
        private readonly StateSaveCommand command;
        TheatreUZContext db;

        public StateSaveCommandHandler(StateSaveCommand command, TheatreUZContext dbContext)
        {
            this.command = command;
            db = dbContext;
        }

        public CommandResponse Execute()
        {
            var response = new CommandResponse()
            {
                Success = false
            };

            try
            {
                var item = db.States.FirstOrDefault(w => w.ID == command.State.ID);

                if (item == null)
                {
                    command.State.ID = Guid.NewGuid();
                    db.States.Add(command.State);
                }
                else
                {
                    db.Entry(item);
                    item.Name = command.State.Name;
                    item.RegDate = command.State.RegDate;
                }

                db.SaveChanges();

                response.ID = item.ID;
                response.Success = true;
                response.Message = "Saved state.";
            }
            catch
            {
                // log error
            }

            return response;
        }
    }

    public class StateDeleteCommandHandler : ICommandHandler<StateDeleteCommand, CommandResponse>
    {
        private readonly StateDeleteCommand command;
        TheatreUZContext db;

        public StateDeleteCommandHandler(StateDeleteCommand command, TheatreUZContext dbContext)
        {
            this.command = command;
            db = dbContext;
        }

        public CommandResponse Execute()
        {
            var response = new CommandResponse()
            {
                Success = false
            };

            try
            {
                var item = db.States.FirstOrDefault(w => w.ID == command.StateID);

                db.States.Remove(item);

                db.SaveChanges();

                response.ID = item.ID;
                response.Success = true;
                response.Message = "Deleted state.";
            }
            catch
            {
                // log error
            }

            return response;
        }
    }
}