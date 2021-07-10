namespace BankerBot
{
    public interface IMessage
    {
        string UserID { get; set; }
        string MessageText { get; set; }
    }
}
