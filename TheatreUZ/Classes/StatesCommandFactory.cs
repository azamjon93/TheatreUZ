using System;
using System.Linq;

namespace TheatreUZ
{
    public static class StateCommandHandlerFactory
    {
        public static ICommandHandler<StateSaveCommand, CommandResponse> Build(StateSaveCommand command)
        {
            return new StateSaveCommandHandler(command);
        }
    }

    public class StateSaveCommandHandler : ICommandHandler<StateSaveCommand, CommandResponse>
    {
        private readonly StateSaveCommand command;

        public StateSaveCommandHandler(StateSaveCommand command)
        {
            this.command = command;
        }

        public CommandResponse Execute()
        {
            var db = new TheatreUZContext();

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
}