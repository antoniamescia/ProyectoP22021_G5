using System;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Este Handler se encarga de dar la bienvenida al usuario, ofreciéndole opciones de acciones a realizar. Además, informa las maneras de finalizar cualquier acción y cómo ver la lista de comandos disponibles.
    /// </summary>
    public class InitHandler : AbstractHandler<UserMessage>
    {
        public InitHandler(InitCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);

            info.ComunicationChannel.SendMessage(request.User, "¡Bienvenido a BankerBot! 💰");
            info.ComunicationChannel.SendMessage(request.User, "¿Qué deseas hacer?:\n" + Commands.Instance.CommandList(request.User));
            info.ComunicationChannel.SendMessage(request.User, "Recuerda que puedes escribir /Salir en cualquier momento para finalizar la acción o /Comandos para ver los comandos disponibles. 😉");

            info.ConversationState = ConversationState.Messenger;
        }
    }
}