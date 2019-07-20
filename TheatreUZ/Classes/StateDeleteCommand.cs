using System;

namespace TheatreUZ
{
    public class StateDeleteCommand : ICommand<CommandResponse>
    {
        public Guid StateID { get; private set; }

        public StateDeleteCommand(Guid id)
        {
            StateID = id;
        }
    }
}