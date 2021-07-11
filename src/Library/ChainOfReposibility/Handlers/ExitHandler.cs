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
        
        /*
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método HandleRequest.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa el método polimórfico handleRequest.
        Cumple con el patrón Chain of Responsibility.
        */
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