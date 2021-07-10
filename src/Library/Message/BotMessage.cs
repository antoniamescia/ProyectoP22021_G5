namespace BankerBot
{
    public class BotMessage : IMessage
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public BotMessage(string id, string message)
        {
            this.Id = id;
            this.Text = message;
        }
    }
}