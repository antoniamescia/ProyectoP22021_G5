namespace Bankbot
{
    public interface IMessage
    {
        string Id { get; set; }
        string Text { get; set; }
    }
}
