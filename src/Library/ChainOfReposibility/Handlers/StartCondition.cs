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
            Data data = Session.Instance.GetChat(request.Id);

            data.Channel.SendMessage(request.Id, "¡Bienvenido a BankerBot! 💰");
            data.Channel.SendMessage(request.Id, "¿Qué deseas hacer?:\n" + Commands.Instance.ListCommands(request.Id));
            data.Channel.SendMessage(request.Id, "Recuerda que puedes escribir /Salir en cualquier momento para finalizar la acción o /Comandos para ver los comandos disponibles. 😉");

            data.State = State.Messenger;
        }
    }
}