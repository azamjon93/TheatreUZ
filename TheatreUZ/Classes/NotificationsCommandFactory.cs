using System;
using System.Linq;

namespace TheatreUZ
{
    public static class NotificationSaveCommandHandlerFactory
    {
        public static ICommandHandler<NotificationSaveCommand, CommandResponse> Build(NotificationSaveCommand command, TheatreUZContext dbContext)
        {
            return new NotificationSaveCommandHandler(command, dbContext);
        }
    }

    public static class NotificationDeleteCommandHandlerFactory
    {
        public static ICommandHandler<NotificationDeleteCommand, CommandResponse> Build(NotificationDeleteCommand command, TheatreUZContext dbContext)
        {
            return new NotificationDeleteCommandHandler(command, dbContext);
        }
    }

    public class NotificationSaveCommandHandler : ICommandHandler<NotificationSaveCommand, CommandResponse>
    {
        private readonly NotificationSaveCommand command;
        TheatreUZContext db;

        public NotificationSaveCommandHandler(NotificationSaveCommand command, TheatreUZContext dbContext)
        {
            this.command = command;
            db = dbContext;
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
                var item = db.Notifications.FirstOrDefault(w => w.ID == command.Notification.ID);

                if (item == null)
                {
                    command.Notification.ID = Guid.NewGuid();
                    db.Notifications.Add(command.Notification);
                }
                else
                {
                    db.Entry(item);
                    item.Message = command.Notification.Message;
                    item.UserID = command.Notification.UserID;
                    item.StateID = command.Notification.StateID;
                    item.RegDate = command.Notification.RegDate;
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

    public class NotificationDeleteCommandHandler : ICommandHandler<NotificationDeleteCommand, CommandResponse>
    {
        private readonly NotificationDeleteCommand command;
        TheatreUZContext db;

        public NotificationDeleteCommandHandler(NotificationDeleteCommand command, TheatreUZContext dbContext)
        {
            this.command = command;
            db = dbContext;
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
                var item = db.Notifications.FirstOrDefault(w => w.ID == command.NotificationID);

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