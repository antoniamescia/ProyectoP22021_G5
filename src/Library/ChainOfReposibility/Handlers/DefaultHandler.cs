using System;
using System.Collections.Generic;

namespace Bankbot
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/
    /// <summary>
    /// Handler por defecto en caso que no se den las otras posibilidades.
    /// </summary>
    public class DefaultHandler : AbstractHandler<IMessage>
    {
        public DefaultHandler(DefaultCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);
            data.Channel.SendMessage(request.Id, "¡Lo siento! No te entendí. 🙃");
            data.State = State.Messenger;
        }
    }
}