using System;

namespace BankerBot
{
    public class PrinterHandler : AbstractHandler<IMessage>
    {
        public PrinterHandler(PrinterCondition condition) : base(condition)
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
                    data.ProvisionalInfo.Add("account", data.User.Accounts[index - 1]);
                    Account account = data.GetDictionaryValue<Account>("account");
                    data.ComunicationChannel.SendMessage(request.UserID, "Cargando... ⌛️");
                    string path = Session.Instance.Printer.Print(account.TransactionsRecord, data.User.Username);
                    data.ComunicationChannel.SendPrint(request.UserID, path);
                    data.ClearOperation();
                    return;
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingresa el índice correcto.");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Qué historial de transacciones deseas ver?:\n" + data.User.DisplayAccounts());
                }
                return;
            }

        }
    }
}