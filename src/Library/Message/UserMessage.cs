using System;

namespace Library
{
    public class UserMessage
    {
        public string User { get; set; }
        public string MessageText { get; set; }

        public UserMessage(string user, string message)
        {
            this.User = user;
            this.MessageText = message;
        }
    }
}
