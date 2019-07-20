using TheatreUZ.Models;

namespace TheatreUZ
{
    public class StateSaveCommand : ICommand<CommandResponse>
    {
        public State State { get; private set; }

        public StateSaveCommand(State item)
        {
            State = item;
        }
    }
}