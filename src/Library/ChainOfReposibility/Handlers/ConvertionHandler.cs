using System;

namespace Bankbot
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/

    /// <summary>
    /// Handler para convertir un tipo de moneda.
    /// </summary>
    public class ConvertionHandler : AbstractHandler<IMessage>
    {
        public ConvertionHandler(ConvertionCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);

            if (!data.Temp.ContainsKey("from"))
            {
                int index;
                if (Int32.TryParse(request.Text, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    data.Temp.Add("from", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    data.Channel.SendMessage(request.Id, "¿A qué moneda deseas convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "Selecciona el índice, por favor.");
                    data.Channel.SendMessage(request.Id, "¿Desde qué moneda quieres convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
            }
            else if (!data.Temp.ContainsKey("to"))
            {
                int index;
                if (Int32.TryParse(request.Text, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    if (CurrencyExchanger.Instance.CurrencyList[index - 1] != data.GetDictionaryValue<Currency>("from"))
                    {
                        data.Temp.Add("to", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                        data.Channel.SendMessage(request.Id, "¿Cuánto es el monto a convertir? ❓");
                    }
                    else
                    {
                        data.Channel.SendMessage(request.Id, "¡Selecciona otra moneda! 🪙");
                        data.Channel.SendMessage(request.Id, "¿Desde qué moneda quieres convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                    }
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "Selecciona el índice, por favor.");
                    data.Channel.SendMessage(request.Id, "¿Desde qué moneda quieres convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
            }
            else if (!data.Temp.ContainsKey("amount"))
            {
                double amount;
                if (double.TryParse(request.Text, out amount) && amount > 0)
                {
                    data.Temp.Add("amount", amount);
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "¡Ingresa un valor mayor a 0!");
                    data.Channel.SendMessage(request.Id, "¿Cuánto es el monto a convertir? ❓");
                }
            }

            if (data.Temp.ContainsKey("from") && data.Temp.ContainsKey("to") && data.Temp.ContainsKey("amount"))
            {
                var amount = data.GetDictionaryValue<double>("amount");
                var from = data.GetDictionaryValue<Currency>("from");
                var to = data.GetDictionaryValue<Currency>("to");

                var newAmount = CurrencyExchanger.Instance.Convert(amount, from, to);
                data.Channel.SendMessage(request.Id, $"¡Conversión exitosa! 🙌 {from.Code} {amount} equivalen a {to.Code} {newAmount}. 🤑");

                data.ClearOperation();
            }
        }
    }
}