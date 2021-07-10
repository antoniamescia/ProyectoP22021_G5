using System;
using System.Collections.Generic;

namespace BankerBot
{
    public class DefaultHandler : AbstractHandler<IMessage>
    {
        public DefaultHandler(DefaultCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);
            data.Channel.SendMessage(request.UserID, "¡Lo siento! No te entendí. 🙃");
            data.State = State.Messenger;
        }
    }
}