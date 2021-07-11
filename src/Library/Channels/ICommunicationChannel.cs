namespace BankerBot
{
    public interface ICommunicationChannel
    {
        void StartCommunication();
        void SendMessage(string id, string message);
        void SendPrint(string id, string path);
        void HandleMessage(IMessage message);

    }
}