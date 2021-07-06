using System;

namespace BankerBot
{
    public class AmountHandler : AbstractHandler<UserMessage>
    {
        public AmountHandler(AmountCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);

            if (!data.ProvisionalInfo.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= data.User.Accounts.Count)
                {
                    Account account = data.User.Accounts[index - 1];
                    data.ComunicationChannel.SendMessage(request.User, $"El balance actual de esta cuenta es: {account.CurrencyType} {account.Amount}");

                    data.ClearOperation();
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "//"); //REVISAR!
                    data.ComunicationChannel.SendMessage(request.User, "¿De qué cuenta deseas consultar el balance? \n" + data.User.DisplayAccounts());
                }
                return;
            }
        }
    }
}