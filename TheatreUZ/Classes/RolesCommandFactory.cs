using System;
using System.Linq;

namespace TheatreUZ
{
    public static class RoleSaveCommandHandlerFactory
    {
        public static ICommandHandler<RoleSaveCommand, CommandResponse> Build(RoleSaveCommand command)
        {
            return new RoleSaveCommandHandler(command);
        }
    }

    public static class RoleDeleteCommandHandlerFactory
    {
        public static ICommandHandler<RoleDeleteCommand, CommandResponse> Build(RoleDeleteCommand command)
        {
            return new RoleDeleteCommandHandler(command);
        }
    }

    public class RoleSaveCommandHandler : ICommandHandler<RoleSaveCommand, CommandResponse>
    {
        private readonly RoleSaveCommand command;

        public RoleSaveCommandHandler(RoleSaveCommand command)
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
                var item = db.Roles.FirstOrDefault(w => w.ID == command.Role.ID);

                if (item == null)
                {
                    command.Role.ID = Guid.NewGuid();
                    db.Roles.Add(command.Role);
                }
                else
                {
                    db.Entry(item);
                    item.Name = command.Role.Name;
                    item.StateID = command.Role.StateID;
                    item.RegDate = command.Role.RegDate;
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

    public class RoleDeleteCommandHandler : ICommandHandler<RoleDeleteCommand, CommandResponse>
    {
        private readonly RoleDeleteCommand command;

        public RoleDeleteCommandHandler(RoleDeleteCommand command)
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
                var item = db.Roles.FirstOrDefault(w => w.ID == command.RoleID);

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