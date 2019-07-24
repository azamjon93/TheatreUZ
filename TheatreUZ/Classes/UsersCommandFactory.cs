using System;
using System.Collections.Generic;
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
                    item = command.User;
                    item.ID = Guid.NewGuid();
                    item.RegDate = DateTime.Now;
                    item.PasswordHash = OwnSecurity.Hash(item.PasswordHash);
                    item.State = db.States.Where(s => s.Name == "Active").FirstOrDefault();
                    item.StateID = db.States.Where(s => s.Name == "Active").FirstOrDefault().ID;

                    if (item.Role == null)
                    {
                        item.Role = db.Roles.Where(r => r.Name == "User").FirstOrDefault();
                        item.RoleID = db.Roles.Where(r => r.Name == "User").FirstOrDefault().ID;
                    }
                    db.Users.Add(item);
                }
                else
                {
                    db.Entry(item);
                    item.Name = command.User.Name;
                    item.Email = command.User.Email;
                    item.RoleID = command.User.RoleID;
                    item.StateID = command.User.StateID;
                    item.RegDate = command.User.RegDate;
                }

                db.SaveChanges();

                response.ID = item.ID;
                response.Success = true;
                response.ResponseObjects = new List<object>
                {
                    item.Name,
                    item.Role
                };
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