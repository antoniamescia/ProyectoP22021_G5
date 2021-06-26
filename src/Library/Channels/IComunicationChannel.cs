using System;

namespace Library
{
    public interface IComunicationChannel
    {
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio.
        Cumple con OCP porque permite la introducción de nuevos tipos de canales de comunicación sin modificar el código existente (se agregan como nuevas clases).
        */
        void StartCommunication();
        void ManageMessage(UserMessage message);
        void SendMessage(UserMessage message);
    }
}
