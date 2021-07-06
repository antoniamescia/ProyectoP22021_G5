using System;


namespace BankerBot
{
    public class AbortHandler : AbstractHandler<UserMessage>
    {
        public AbortHandler(AbortCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.User);

            if (data.Command != string.Empty)
            {
                data.ComunicationChannel.SendMessage(request.User, "¡Operación cancelada! ❌");
                data.ClearOperation();
            }
            else
            {
                data.ComunicationChannel.SendMessage(request.User, "¡Lo siento! Esta operación no puede cancelarse pues no está dentro de los comandos.");
            }
        }
    }
}