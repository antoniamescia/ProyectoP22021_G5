using System;

namespace Library
{

    public class ConvertionHandler : AbstractHandler<UserMessage>
    {
        public ConvertionHandler(ConvertionCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);

            if (!info.ProvisionalInfo.ContainsKey("from"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    info.ProvisionalInfo.Add("from", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    info.ComunicationChannel.SendMessage(request.User, "¿A qué moneda quieres convertir? 🪙 \n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "//");
                    info.ComunicationChannel.SendMessage(request.User, "¿A qué moneda quieres convertir? 🪙:\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
            }
            else if (!info.ProvisionalInfo.ContainsKey("to"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    if (CurrencyExchanger.Instance.CurrencyList[index - 1] != info.GetDictionaryValue<Currency>("from"))
                    {
                        info.ProvisionalInfo.Add("to", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                        info.ComunicationChannel.SendMessage(request.User, "Monto a convertir:");
                    }
                    else
                    {
                        info.ComunicationChannel.SendMessage(request.User, "Selecciona una moneda diferente, por favor.");
                        info.ComunicationChannel.SendMessage(request.User, "¿A qué moneda quieres convertir? 🪙:\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                    }
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "//");
                    info.ComunicationChannel.SendMessage(request.User, "¿A qué moneda quieres convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
            }
            else if (!info.ProvisionalInfo.ContainsKey("amount"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0)
                {
                    info.ProvisionalInfo.Add("amount", amount);
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese un valor mayor a 0. ⚠️");
                    info.ComunicationChannel.SendMessage(request.User, "Monto a convertir:");
                }
            }

            if (info.ProvisionalInfo.ContainsKey("from") && info.ProvisionalInfo.ContainsKey("to") && info.ProvisionalInfo.ContainsKey("amount"))
            {
                var amount = info.GetDictionaryValue<double>("amount");
                var from = info.GetDictionaryValue<Currency>("from");
                var to = info.GetDictionaryValue<Currency>("to");

                var newAmount = CurrencyExchanger.Instance.Convert(amount, from, to);
                info.ComunicationChannel.SendMessage(request.User, $"{from.Type} {amount} equivalen a {to.Type} {newAmount}");

                info.ClearOperation();
            }
        }
    }
}