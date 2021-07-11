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
        
        /*
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método HandleRequest.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa el método polimórfico handleRequest.
        Cumple con el patrón Chain of Responsibility.
        */
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