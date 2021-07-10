namespace BankerBot
{
    public class BotMessage : IMessage
    {
        public string UserID { get; set; }
        public string MessageText { get; set; }
        public BotMessage(string id, string message)
        {
            this.UserID = id;
            this.MessageText = message;
        }
    }
}