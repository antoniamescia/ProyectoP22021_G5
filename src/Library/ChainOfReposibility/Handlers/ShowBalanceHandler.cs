using System;

namespace BankerBot
{
    public class ShowBalanceHandler : AbstractHandler<IMessage>
    {
        /// <summary>
        /// Handler que se encargará de mostrar el balance actual de cuenta particular.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        
        /*
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método HandleRequest.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa el método polimórfico handleRequest.
        Cumple con el patrón Chain of Responsibility.
        */
        public ShowBalanceHandler(ShowBalanceCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);

            if (!data.ProvisionalInfo.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= data.User.Accounts.Count)
                {
                    Account account = data.User.Accounts[index - 1];
                    data.ComunicationChannel.SendMessage(request.UserID, $"El balance actual de la cuenta es: {account.CurrencyType.Code} {account.Amount}");
                    data.ClearOperation();
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingresa el índice, por favor.");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿De qué cuenta quieres consultar el balance?:\n" + data.User.DisplayAccounts());
                }
                return;
            }
        }
    }
}