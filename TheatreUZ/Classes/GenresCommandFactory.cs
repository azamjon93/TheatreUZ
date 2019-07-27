using System;
using System.Linq;

namespace TheatreUZ
{
    public static class GenreSaveCommandHandlerFactory
    {
        public static ICommandHandler<GenreSaveCommand, CommandResponse> Build(GenreSaveCommand command, TheatreUZContext dbContext)
        {
            return new GenreSaveCommandHandler(command, dbContext);
        }
    }

    public static class GenreDeleteCommandHandlerFactory
    {
        public static ICommandHandler<GenreDeleteCommand, CommandResponse> Build(GenreDeleteCommand command, TheatreUZContext dbContext)
        {
            return new GenreDeleteCommandHandler(command, dbContext);
        }
    }

    public class GenreSaveCommandHandler : ICommandHandler<GenreSaveCommand, CommandResponse>
    {
        private readonly GenreSaveCommand command;
        TheatreUZContext db;

        public GenreSaveCommandHandler(GenreSaveCommand cmd, TheatreUZContext dbContext)
        {
            command = cmd;
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
                var item = db.Genres.FirstOrDefault(w => w.ID == command.Genre.ID);

                if (item == null)
                {
                    command.Genre.ID = Guid.NewGuid();
                    db.Genres.Add(command.Genre);
                }
                else
                {
                    item.Name = command.Genre.Name;
                    item.StateID = command.Genre.StateID;
                    item.RegDate = command.Genre.RegDate;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
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
        TheatreUZContext db;

        public GenreDeleteCommandHandler(GenreDeleteCommand cmd, TheatreUZContext dbContext)
        {
            command = cmd;
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