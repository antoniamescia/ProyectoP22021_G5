using System;
using System.Collections.Generic;

namespace BankerBot
{
    public class StartConversationHandler : AbstractHandler<IMessage>
    {
        
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