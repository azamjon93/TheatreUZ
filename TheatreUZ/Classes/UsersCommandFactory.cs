using System;
using System.Linq;

namespace TheatreUZ
{
    public static class UserSaveCommandHandlerFactory
    {
        public static ICommandHandler<UserSaveCommand, CommandResponse> Build(UserSaveCommand command)
        {
            return new UserSaveCommandHandler(command);
        }
    }

    public static class UserDeleteCommandHandlerFactory
    {
        public static ICommandHandler<UserDeleteCommand, CommandResponse> Build(UserDeleteCommand command)
        {
            return new UserDeleteCommandHandler(command);
        }
    }

    public class UserSaveCommandHandler : ICommandHandler<UserSaveCommand, CommandResponse>
    {
        private readonly UserSaveCommand command;

        public UserSaveCommandHandler(UserSaveCommand command)
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
                var item = db.Users.FirstOrDefault(w => w.ID == command.User.ID);

                if (item == null)
                {
                    command.User.ID = Guid.NewGuid();
                    db.Users.Add(command.User);
                }
                else
                {
                    db.Entry(item);
                    item.Name = command.User.Name;
                    item.StateID = command.User.StateID;
                    item.RoleID = command.User.RoleID;
                    item.RegDate = command.User.RegDate;
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

    public class UserDeleteCommandHandler : ICommandHandler<UserDeleteCommand, CommandResponse>
    {
        private readonly UserDeleteCommand command;

        public UserDeleteCommandHandler(UserDeleteCommand command)
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
                var item = db.Users.FirstOrDefault(w => w.ID == command.UserID);

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