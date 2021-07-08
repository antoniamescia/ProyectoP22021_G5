using System;

namespace Bankbot
{
    public class BalanceHandler : AbstractHandler<IMessage>
    {
        public BalanceHandler(BalanceCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);

            if (!data.Temp.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.Text, out index) && index > 0 && index <= data.User.Accounts.Count)
                {
                    var account = data.User.Accounts[index - 1];
                    data.Channel.SendMessage(request.Id, $"El balance actual de la cuenta es: {account.Currency.Code} {account.Balance}");

                    data.ClearOperation();
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "Ingresa el índice, por favor.");
                    data.Channel.SendMessage(request.Id, "¿De qué cuenta quieres consultar el balance?:\n" + data.User.ShowAccountList());
                }
                return;
            }
        }
    }
}