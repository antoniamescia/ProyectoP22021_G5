using System;

namespace Library
{
    public interface IComunicationChannel
    {
        void StartCommunication();
        void ManageMessage(Message message);
        void SendMessage(Message message);
    }
}
