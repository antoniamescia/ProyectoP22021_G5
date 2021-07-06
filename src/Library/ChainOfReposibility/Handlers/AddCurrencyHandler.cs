using System;

namespace BankerBot
{
    public class AddCurrencyHandler : AbstractHandler<UserMessage>
    {
        public AddCurrencyHandler(AddCurrencyCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);

            if (!data.ProvisionalInfo.ContainsKey("")) //REVISAR
            {
                data.ProvisionalInfo.Add("", request.MessageText); //REVISAR
                data.ComunicationChannel.SendMessage(request.User, "Ingresa el símbolo de la nueva moneda 💲: ");
            }
            else if (!data.ProvisionalInfo.ContainsKey("symbol")) //REVISAR
            {
                data.ProvisionalInfo.Add("symbol", request.MessageText); //REVISAR
                data.ComunicationChannel.SendMessage(request.User, "Ingresa la taza de cambio basada en pesos uruguayos (UYU) 🇺🇾:");
            }
            else if (!data.ProvisionalInfo.ContainsKey("rate"))
            {
                double rate;
                if (double.TryParse(request.MessageText, out rate) && rate > 0)
                {
                    data.ProvisionalInfo.Add("rate", rate);
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Ingresa un valor mayor a cero. ⚠️");
                    data.ComunicationChannel.SendMessage(request.User, "Ingresa la taza de cambio basada en pesos uruguayos (UYU) 🇺🇾:");
                }
            }

            if (data.ProvisionalInfo.ContainsKey("iso") && data.ProvisionalInfo.ContainsKey("symbol") && data.ProvisionalInfo.ContainsKey("rate"))
            {
                var symbol = data.GetDictionaryValue<string>("symbol");
                var rate = data.GetDictionaryValue<double>("rate");

                if (!CurrencyExchanger.Instance.ExistsCurrency(symbol)) //REVISAR!
                {
                    CurrencyExchanger.Instance.AddCurrency(symbol, rate);
                    data.ComunicationChannel.SendMessage(request.User, "¡Moneda agregada con éxito! 🙌");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Esta moneda ya existe, pero puedes crear una nueva con el comando /agregarmoneda. 😉");
                }
                data.ClearOperation();
            }
        }
    }
}