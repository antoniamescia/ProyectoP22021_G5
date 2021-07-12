using System;

namespace BankerBot
{
    public class PrinterHandler : AbstractHandler<IMessage>
    {
        /*
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método HandleRequest.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa el método polimórfico handleRequest.
        Cumple con el patrón Chain of Responsibility.
        */
        
        /// <summary>
        /// Handler que se encarga de la impresora HTML
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
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
                    data.ComunicationChannel.SendMessage(request.UserID, "Cargando...");
                    string path = Session.Instance.Printer.Print(account.TransactionsRecord, data.User.Username);
                    data.ComunicationChannel.SendPrint(request.UserID, path);
                    data.ClearOperation();
                    return;
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "Debe ingresar un indice correcto.");
                    data.ComunicationChannel.SendMessage(request.UserID, "Seleccione una cuenta para ver el historial:\n" + data.User.DisplayAccounts());
                }
                return;
            }

        }
    }
}