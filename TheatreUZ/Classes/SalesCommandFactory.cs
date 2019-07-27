using System;
using System.Linq;

namespace TheatreUZ
{
    public static class SaleSaveCommandHandlerFactory
    {
        public static ICommandHandler<SaleSaveCommand, CommandResponse> Build(SaleSaveCommand command, TheatreUZContext dbContext)
        {
            return new SaleSaveCommandHandler(command, dbContext);
        }
    }

    public static class SaleDeleteCommandHandlerFactory
    {
        public static ICommandHandler<SaleDeleteCommand, CommandResponse> Build(SaleDeleteCommand command, TheatreUZContext dbContext)
        {
            return new SaleDeleteCommandHandler(command, dbContext);
        }
    }

    public class SaleSaveCommandHandler : ICommandHandler<SaleSaveCommand, CommandResponse>
    {
        private readonly SaleSaveCommand command;
        TheatreUZContext db;

        public SaleSaveCommandHandler(SaleSaveCommand command, TheatreUZContext dbContext)
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
                var item = db.Sales.FirstOrDefault(w => w.ID == command.Sale.ID);

                if (item == null)
                {
                    item = command.Sale;
                    command.Sale.ID = Guid.NewGuid();
                    command.Sale.User = db.Users.Where(u => u.ID == item.UserID).FirstOrDefault();
                    command.Sale.Spectacle = db.Spectacles.Where(s => s.ID == item.SpectacleID).FirstOrDefault();
                    command.Sale.State = db.States.Where(s => s.Name == "Active").FirstOrDefault();
                    command.Sale.StateID = command.Sale.State.ID;
                    command.Sale.RegDate = DateTime.Now;

                    db.Sales.Add(command.Sale);
                }
                else
                {
                    db.Entry(item);
                    item.Amount = command.Sale.Amount;
                    item.UserID = command.Sale.UserID;
                    item.SpectacleID = command.Sale.SpectacleID;
                    item.StateID = command.Sale.StateID;
                    item.RegDate = command.Sale.RegDate;
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

    public class SaleDeleteCommandHandler : ICommandHandler<SaleDeleteCommand, CommandResponse>
    {
        private readonly SaleDeleteCommand command;
        TheatreUZContext db;

        public SaleDeleteCommandHandler(SaleDeleteCommand command, TheatreUZContext dbContext)
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
                var item = db.Sales.FirstOrDefault(w => w.ID == command.SaleID);

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