using System;

namespace BankerBot
{
    /// <summary>
    /// Handler que se encargará de hacer la conversión de monedas.
    /// </summary>
    public class ConvertionHandler : AbstractHandler<IMessage>
    {
        public ConvertionHandler(ConvertionCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);

            if (!data.ProvisionalInfo.ContainsKey("initialCurrency"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    data.ProvisionalInfo.Add("initialCurrency", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    data.ComunicationChannel.SendMessage(request.UserID, "¿A qué moneda deseas convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "Selecciona el índice, por favor.");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Desde qué moneda quieres convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
            }
            else if (!data.ProvisionalInfo.ContainsKey("finalCurrency"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    if (CurrencyExchanger.Instance.CurrencyList[index - 1] != data.GetDictionaryValue<Currency>("initialCurrency"))
                    {
                        data.ProvisionalInfo.Add("finalCurrency", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                        data.ComunicationChannel.SendMessage(request.UserID, "¿Cuánto es el monto a convertir? ❓");
                    }
                    else
                    {
                        data.ComunicationChannel.SendMessage(request.UserID, "¡Selecciona otra moneda! 🪙");
                        data.ComunicationChannel.SendMessage(request.UserID, "¿Desde qué moneda quieres convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                    }
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "Selecciona el índice, por favor.");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Desde qué moneda quieres convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
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
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Ingresa un valor mayor a 0!");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Cuánto es el monto a convertir? ❓");
                }
            }

            if (data.ProvisionalInfo.ContainsKey("initialCurrency") && data.ProvisionalInfo.ContainsKey("finalCurrency") && data.ProvisionalInfo.ContainsKey("amount"))
            {
                double amount = data.GetDictionaryValue<double>("amount");
                Currency initialCurrency = data.GetDictionaryValue<Currency>("initialCurrency");
                Currency finalCurrency = data.GetDictionaryValue<Currency>("finalCurrency");

                double newAmount = CurrencyExchanger.Instance.Convert(amount, initialCurrency, finalCurrency);
                data.ComunicationChannel.SendMessage(request.UserID, $"¡Conversión exitosa! 🙌 {initialCurrency.Code} {amount} equivalen a {finalCurrency.Code} {newAmount}. 🤑");

                data.ClearOperation();
            }
        }
    }
}