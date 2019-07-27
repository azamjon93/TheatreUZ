using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace TheatreUZ
{
    public static class SpectacleSaveCommandHandlerFactory
    {
        public static ICommandHandler<SpectacleSaveCommand, CommandResponse> Build(SpectacleSaveCommand command, TheatreUZContext dbContext)
        {
            return new SpectacleSaveCommandHandler(command, dbContext);
        }
    }

    public static class SpectacleDeleteCommandHandlerFactory
    {
        public static ICommandHandler<SpectacleDeleteCommand, CommandResponse> Build(SpectacleDeleteCommand command, TheatreUZContext dbContext)
        {
            return new SpectacleDeleteCommandHandler(command, dbContext);
        }
    }

    public class SpectacleSaveCommandHandler : ICommandHandler<SpectacleSaveCommand, CommandResponse>
    {
        private readonly SpectacleSaveCommand command;
        TheatreUZContext db;

        public SpectacleSaveCommandHandler(SpectacleSaveCommand command, TheatreUZContext dbContext)
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
                var item = db.Spectacles.FirstOrDefault(w => w.ID == command.Spectacle.ID);

                command.Spectacle.Genre = db.Genres.Where(g => g.ID == command.Spectacle.GenreID).FirstOrDefault();
                command.Spectacle.State = db.States.Where(s => s.ID == command.Spectacle.StateID).FirstOrDefault();

                if (item == null)
                {
                    command.Spectacle.ID = Guid.NewGuid();
                    db.Spectacles.Add(command.Spectacle);
                }
                else
                {
                    if (item.PlayDate != command.Spectacle.PlayDate || item.GenreID != command.Spectacle.GenreID ||
                        item.Cost != command.Spectacle.Cost || item.Name != command.Spectacle.Name ||
                        item.StateID != command.Spectacle.StateID)
                    {
                        SmtpClient client = new SmtpClient()
                        {
                            EnableSsl = true,
                            Host = "smtp.mail.ru",
                            Credentials = new NetworkCredential("ctou-test@mail.ru", "testpassword123")
                        };

                        List<string> emails = db.Sales.Where(s => s.SpectacleID == command.Spectacle.ID).Select(u => u.User.Email).ToList();

                        MailMessage msg = new MailMessage
                        {
                            From = new MailAddress("ctou-test@mail.ru"),
                            Subject = "Warning from Central theatre of Uzbekistan",
                            Body = string.Format("Some informations of spectacle which you booked recently was changed.\nOld information:\nName: {0}\nGenre: {1}\nPlay date: {2}\nCost: {3}\n\nNew information:\nName: {4}\nGenre: {5}\nPlay date: {6}\nCost: {7}",
                                                    item.Name, item.Genre.Name, item.PlayDate, item.Cost,
                                                    command.Spectacle.Name, command.Spectacle.Genre.Name, command.Spectacle.PlayDate, command.Spectacle.Cost)
                        };

                        emails.ForEach(e => msg.To.Add(e));

                        client.Send(msg);
                    }

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
        TheatreUZContext db;

        public SpectacleDeleteCommandHandler(SpectacleDeleteCommand command, TheatreUZContext dbContext)
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