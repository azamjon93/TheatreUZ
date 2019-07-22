using TheatreUZ.Models;

namespace TheatreUZ
{
    public class SaleSaveCommand : ICommand<CommandResponse>
    {
        public Sale Sale { get; private set; }

        public SaleSaveCommand(Sale item)
        {
            Sale = item;
        }
    }
}