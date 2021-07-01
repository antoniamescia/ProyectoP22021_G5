using System;

namespace BankerBot
{
    public class ConsoleBot : IComunicationChannel
    {
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio.
        Cumple con LSP porque el tipo implícito que define la clase puede ser sustiuido por ICommunicationChannel.
        Cumple con ISP porque solo implementa una interfaz (ICommunicationChannel).
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con las responsabilidades otorgadas.
        Cumple con Polymorphism porque usa los métodos polimórfico StatCommunication, ManageMessage y SendMessage.
        */
        public void StartCommunication()
        {
            
        }
        public void ManageMessage(UserMessage message)
        {

        }
        public void SendMessage(UserMessage message)
        {

        }

    }
}
