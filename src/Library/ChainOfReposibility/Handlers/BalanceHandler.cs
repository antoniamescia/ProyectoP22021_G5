using System;

namespace BankerBot
{
    public class BalanceHandler : AbstractHandler<IMessage>
    {
        public BalanceHandler(BalanceCondition condition) : base(condition)
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
                    var account = data.User.Accounts[index - 1];
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