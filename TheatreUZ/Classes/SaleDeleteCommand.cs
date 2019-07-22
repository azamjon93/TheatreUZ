using System;

namespace TheatreUZ
{
    public class SaleDeleteCommand : ICommand<CommandResponse>
    {
        public Guid SaleID { get; private set; }

        public SaleDeleteCommand(Guid id)
        {
            SaleID = id;
        }
    }
}