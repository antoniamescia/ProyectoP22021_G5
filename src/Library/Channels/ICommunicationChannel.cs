namespace BankerBot
{
    public interface ICommunicationChannel
    {
        void StartCommunication();
        void HandleMessage(IMessage message);
        void SendMessage(string id, string message);

    }
}