using System;
using System.Linq;

namespace TheatreUZ
{
    public static class GenreSaveCommandHandlerFactory
    {
        public static ICommandHandler<GenreSaveCommand, CommandResponse> Build(GenreSaveCommand command)
        {
            return new GenreSaveCommandHandler(command);
        }
    }

    public static class GenreDeleteCommandHandlerFactory
    {
        public static ICommandHandler<GenreDeleteCommand, CommandResponse> Build(GenreDeleteCommand command)
        {
            return new GenreDeleteCommandHandler(command);
        }
    }

    public class GenreSaveCommandHandler : ICommandHandler<GenreSaveCommand, CommandResponse>
    {
        private readonly GenreSaveCommand command;

        public GenreSaveCommandHandler(GenreSaveCommand command)
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
                var item = db.Genres.FirstOrDefault(w => w.ID == command.Genre.ID);

                if (item == null)
                {
                    command.Genre.ID = Guid.NewGuid();
                    db.Genres.Add(command.Genre);
                }
                else
                {
                    db.Entry(item);
                    item.Name = command.Genre.Name;
                    item.StateID = command.Genre.StateID;
                    item.RegDate = command.Genre.RegDate;
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

    public class GenreDeleteCommandHandler : ICommandHandler<GenreDeleteCommand, CommandResponse>
    {
        private readonly GenreDeleteCommand command;

        public GenreDeleteCommandHandler(GenreDeleteCommand command)
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
                var item = db.Genres.FirstOrDefault(w => w.ID == command.GenreID);

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