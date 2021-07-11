using System;


namespace BankerBot
{
    public class ExitHandler : AbstractHandler<IMessage>
    {
        /// <summary>
        /// Handler que se encargará de finalizar una acción cuando se le indica.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ExitHandler(ExitCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);

            if (data.Command != string.Empty)
            {
                data.ComunicationChannel.SendMessage(request.UserID, "¡Operación cancelada! ❌");
            }
        }
    }
}