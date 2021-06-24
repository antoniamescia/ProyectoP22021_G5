using System;

namespace Library
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
