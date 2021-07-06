using System;

namespace BankerBot
{

    public class ConvertionHandler : AbstractHandler<UserMessage>
    {
        public ConvertionHandler(ConvertionCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.User);

            if (!data.ProvisionalInfo.ContainsKey("from"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    data.ProvisionalInfo.Add("from", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    data.ComunicationChannel.SendMessage(request.User, "¿A qué moneda quieres convertir? 🪙 \n" + CurrencyExchanger.Instance.ShowCurrencyList());
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "//");
                    data.ComunicationChannel.SendMessage(request.User, "¿A qué moneda quieres convertir? 🪙:\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                }
            }
            else if (!data.ProvisionalInfo.ContainsKey("to"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    if (CurrencyExchanger.Instance.CurrencyList[index - 1] != data.GetDictionaryValue<Currency>("from"))
                    {
                        data.ProvisionalInfo.Add("to", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                        data.ComunicationChannel.SendMessage(request.User, "Monto a convertir:");
                    }
                    else
                    {
                        data.ComunicationChannel.SendMessage(request.User, "Selecciona una moneda diferente, por favor.");
                        data.ComunicationChannel.SendMessage(request.User, "¿A qué moneda quieres convertir? 🪙:\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                    }
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "//");
                    data.ComunicationChannel.SendMessage(request.User, "¿A qué moneda quieres convertir? 🪙\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                }
            }
            else if (!data.ProvisionalInfo.ContainsKey("amount"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0)
                {
                    data.ProvisionalInfo.Add("amount", amount);
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese un valor mayor a 0. ⚠️");
                    data.ComunicationChannel.SendMessage(request.User, "Monto a convertir:");
                }
            }

            if (data.ProvisionalInfo.ContainsKey("from") && data.ProvisionalInfo.ContainsKey("to") && data.ProvisionalInfo.ContainsKey("amount"))
            {
                var amount = data.GetDictionaryValue<double>("amount");
                var from = data.GetDictionaryValue<Currency>("from");
                var to = data.GetDictionaryValue<Currency>("to");

                var newAmount = CurrencyExchanger.Instance.Convert(amount, from, to);
                data.ComunicationChannel.SendMessage(request.User, $"{from.Type} {amount} equivalen a {to.Type} {newAmount}");

                data.ClearOperation();
            }
        }
    }
}