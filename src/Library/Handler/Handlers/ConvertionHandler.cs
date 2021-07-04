using System;

namespace Library
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/

    /// <summary>
    /// Handler para convertir un tipo de moneda.
    /// </summary>
    public class ConvertionHandler : AbstractHandler<UserMessage>
    {
        public ConvertionHandler(ConvertionCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);

            if (!data.Temp.ContainsKey("from"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    data.Temp.Add("from", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    data.ComunicationChannel.SendMessage(request.User, "Seleccione a que moneda desea convertir:\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Debe seleecionar un valor correspondiente al índice de la moneda.");
                    data.ComunicationChannel.SendMessage(request.User, "Seleccione la moneda desde la que desea convertir:\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                }
            }
            else if (!data.Temp.ContainsKey("to"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    if (CurrencyExchanger.Instance.CurrencyList[index - 1] != data.GetDictionaryValue<Currency>("from"))
                    {
                        data.Temp.Add("to", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                        data.ComunicationChannel.SendMessage(request.User, "Ingrese el monto que desea convertir:");
                    }
                    else
                    {
                        data.ComunicationChannel.SendMessage(request.User, "Debe seleecionar una moneda diferente.");
                        data.ComunicationChannel.SendMessage(request.User, "Seleccione la moneda desde la que desea convertir:\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                    }
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Debe seleecionar un valor correspondiente al índice de la moneda.");
                    data.ComunicationChannel.SendMessage(request.User, "Seleccione la moneda desde la que desea convertir:\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                }
            }
            else if (!data.Temp.ContainsKey("amount"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0)
                {
                    data.Temp.Add("amount", amount);
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Debe ingresar un valor numérico mayor a 0.");
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese el monto que desea convertir:");
                }
            }

            if (data.Temp.ContainsKey("from") && data.Temp.ContainsKey("to") && data.Temp.ContainsKey("amount"))
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