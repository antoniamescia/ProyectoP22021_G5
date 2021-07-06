using System;


namespace Library
{
    public class AbortHandler : AbstractHandler<UserMessage>
    {
        public AbortHandler(AbortCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);

            if (info.Command != string.Empty)
            {
                info.ComunicationChannel.SendMessage(request.User, "¡Operación cancelada! ❌");
                info.ClearOperation();
            }
            else
            {
                info.ComunicationChannel.SendMessage(request.User, "¡Lo siento! Esta operación no puede cancelarse pues no está dentro de los comandos.");
            }
        }
    }
}