using System;
using System.Linq;

namespace TheatreUZ
{
    public static class SpectacleSaveCommandHandlerFactory
    {
        public static ICommandHandler<SpectacleSaveCommand, CommandResponse> Build(SpectacleSaveCommand command)
        {
            return new SpectacleSaveCommandHandler(command);
        }
    }

    public static class SpectacleDeleteCommandHandlerFactory
    {
        public static ICommandHandler<SpectacleDeleteCommand, CommandResponse> Build(SpectacleDeleteCommand command)
        {
            return new SpectacleDeleteCommandHandler(command);
        }
    }

    public class SpectacleSaveCommandHandler : ICommandHandler<SpectacleSaveCommand, CommandResponse>
    {
        private readonly SpectacleSaveCommand command;

        public SpectacleSaveCommandHandler(SpectacleSaveCommand command)
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
                var item = db.Spectacles.FirstOrDefault(w => w.ID == command.Spectacle.ID);

                if (item == null)
                {
                    command.Spectacle.ID = Guid.NewGuid();
                    db.Spectacles.Add(command.Spectacle);
                }
                else
                {
                    db.Entry(item);
                    item.Name = command.Spectacle.Name;
                    item.Cost = command.Spectacle.Cost;
                    item.TicketsCount = command.Spectacle.TicketsCount;
                    item.PlayDate = command.Spectacle.PlayDate;
                    item.GenreID = command.Spectacle.GenreID;
                    item.StateID = command.Spectacle.StateID;
                    item.RegDate = command.Spectacle.RegDate;
                }

                db.SaveChanges();

                response.ID = item.ID;
                response.Success = true;
                response.Message = "Saved state.";
            }
            catch
            {

            }

            return response;
        }
    }

    public class SpectacleDeleteCommandHandler : ICommandHandler<SpectacleDeleteCommand, CommandResponse>
    {
        private readonly SpectacleDeleteCommand command;

        public SpectacleDeleteCommandHandler(SpectacleDeleteCommand command)
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
                var item = db.Spectacles.FirstOrDefault(w => w.ID == command.SpectacleID);

                db.Entry(item);
                item.StateID = db.States.Where(s => s.Name == "Deleted").FirstOrDefault().ID;
                db.SaveChanges();

                response.ID = item.ID;
                response.Success = true;
                response.Message = "Deleted state.";
            }
            catch
            {

            }

            return response;
        }
    }
}