using System;
using System.Collections.Generic;

namespace BankerBot
{
    public class DefaultHandler : AbstractHandler<IMessage>
    {
        /// <summary>
        /// Handler por "default" que se encarga de indicarle al usuario que no comprendió su petición.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DefaultHandler(DefaultCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);
            data.ComunicationChannel.SendMessage(request.UserID, "¡Lo siento! No te entendí. 🙃");
            data.ConversationState = ConversationState.Messenger;
        }
    }
}