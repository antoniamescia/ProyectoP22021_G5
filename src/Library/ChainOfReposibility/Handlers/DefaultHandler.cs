using System;
using System.Collections.Generic;

namespace Library
{
    public class DefaultHandler : AbstractHandler<UserMessage>
    {
        public DefaultHandler(DefaultCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.User);
            data.ComunicationChannel.SendMessage(request.User, "No te entendi, vuelve a intentarlo.");
            data.ConversationState = ConversationState.Messenger;
        }
    }
}