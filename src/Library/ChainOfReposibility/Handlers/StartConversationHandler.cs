using System;
using System.Collections.Generic;

namespace BankerBot
{
    public class StartConversationHandler : AbstractHandler<IMessage>
    {
        /// <summary>
        /// Handler que se encarga de iniciar la conversación.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        
        /*
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método HandleRequest.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa el método polimórfico handleRequest.
        Cumple con el patrón Chain of Responsibility.
        */
        public StartConversationHandler(StartConversationCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);

            data.ComunicationChannel.SendMessage(request.UserID, "¡Bienvenido a BankerBot! 💰");
            data.ComunicationChannel.SendMessage(request.UserID, "¿Qué deseas hacer?:\n" + Commands.Instance.CommandList(request.UserID));
            data.ComunicationChannel.SendMessage(request.UserID, "Recuerda que puedes escribir /Salir en cualquier momento para finalizar la acción o /Comandos para ver los comandos disponibles. 😉");
            data.ConversationState = ConversationState.Messenger;
        }
    }
}