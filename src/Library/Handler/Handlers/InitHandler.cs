using System;
using System.Collections.Generic;

namespace Library
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/
    /// <summary>
    /// Handler de bienvenida.
    /// </summary>
    public class InitHandler : AbstractHandler<UserMessage>
    {
        public InitHandler(InitCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);

            data.ComunicationChannel.SendMessage(request.User, "Bienvenido!");
            data.ComunicationChannel.SendMessage(request.User, "Elija un comando de la siguiente lista:\n" + Commands.Instance.CommandList(request.User));
            data.ComunicationChannel.SendMessage(request.User, "También puedes utilizar el comando /Abort para abortar cualquier actividad que estés realizando o /Commands para ver los comandos disponibles");

            data.State = State.Dispatcher;
        }
    }
}