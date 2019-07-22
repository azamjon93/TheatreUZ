﻿using System;
using System.Linq;

namespace TheatreUZ
{
    public static class SaleSaveCommandHandlerFactory
    {
        public static ICommandHandler<SaleSaveCommand, CommandResponse> Build(SaleSaveCommand command)
        {
            return new SaleSaveCommandHandler(command);
        }
    }

    public static class SaleDeleteCommandHandlerFactory
    {
        public static ICommandHandler<SaleDeleteCommand, CommandResponse> Build(SaleDeleteCommand command)
        {
            return new SaleDeleteCommandHandler(command);
        }
    }

    public class SaleSaveCommandHandler : ICommandHandler<SaleSaveCommand, CommandResponse>
    {
        private readonly SaleSaveCommand command;

        public SaleSaveCommandHandler(SaleSaveCommand command)
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
                var item = db.Sales.FirstOrDefault(w => w.ID == command.Sale.ID);

                if (item == null)
                {
                    command.Sale.ID = Guid.NewGuid();
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

        public SaleDeleteCommandHandler(SaleDeleteCommand command)
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