using System;

namespace Library
{
    public interface IComunicationChannel
    {
        //Cumple con el patrón OCP porque permite agregar nuevos canales de comunicación, es decir nuevos bots, sin necesidad de modificar el código existente.
        //Cumple con SRP pues no se identifica más de una razón de cambio. 
        void StartCommunication();
        void ManageMessage(UserMessage message);
        void SendMessage(UserMessage message);
    }
}
