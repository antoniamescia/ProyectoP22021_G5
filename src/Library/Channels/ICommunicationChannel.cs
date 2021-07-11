namespace BankerBot
{
    public interface ICommunicationChannel
    {
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio.
        Cumple con OCP porque permite la introducción de nuevos tipos de canales de comunicación sin modificar el código existente (se agregan como nuevas clases).
        */
        void StartCommunication();
        void SendMessage(string id, string message);
        void SendPrint(string id, string path);
        void HandleMessage(IMessage message);

    }
}