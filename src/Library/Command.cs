using System;

namespace BankerBot
{
    public class Command
    {

        public Command Commands
        {
            get;
            private set;
        }
        public EndUser EndUser
        {
            get;
        }
        public Command(string command)
        {

        }
    }
}
