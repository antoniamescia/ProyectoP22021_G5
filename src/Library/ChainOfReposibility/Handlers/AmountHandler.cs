using System;

namespace Library
{
    public class AmountHandler : AbstractHandler<UserMessage>
    {
        public AmountHandler(AmountCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);

            if (!info.ProvisionalInfo.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= info.User.Accounts.Count)
                {
                    Account account = info.User.Accounts[index - 1];
                    info.ComunicationChannel.SendMessage(request.User, $"El balance actual de esta cuenta es: {account.CurrencyType} {account.Amount}");

                    info.ClearOperation();
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "//"); //REVISAR!
                    info.ComunicationChannel.SendMessage(request.User, "¿De qué cuenta deseas consultar el balance? \n" + info.User.DisplayAccounts());
                }
                return;
            }
        }
    }
}