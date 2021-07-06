using System;

namespace Library
{
    public class AddCurrencyHandler : AbstractHandler<UserMessage>
    {
        public AddCurrencyHandler(AddCurrencyCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            var info = Session.Instance.GetChatInfo(request.User);

            if (!info.ProvisionalInfo.ContainsKey("")) //REVISAR
            {
                info.ProvisionalInfo.Add("", request.MessageText); //REVISAR
                info.ComunicationChannel.SendMessage(request.User, "Ingresa el símbolo de la nueva moneda 💲: ");
            }
            else if (!info.ProvisionalInfo.ContainsKey("symbol")) //REVISAR
            {
                info.ProvisionalInfo.Add("symbol", request.MessageText); //REVISAR
                info.ComunicationChannel.SendMessage(request.User, "Ingresa la taza de cambio basada en pesos uruguayos (UYU) 🇺🇾:");
            }
            else if (!info.ProvisionalInfo.ContainsKey("rate"))
            {
                double rate;
                if (double.TryParse(request.MessageText, out rate) && rate > 0)
                {
                    info.ProvisionalInfo.Add("rate", rate);
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Ingresa un valor mayor a cero. ⚠️");
                    info.ComunicationChannel.SendMessage(request.User, "Ingresa la taza de cambio basada en pesos uruguayos (UYU) 🇺🇾:");
                }
            }

            if (info.ProvisionalInfo.ContainsKey("iso") && info.ProvisionalInfo.ContainsKey("symbol") && info.ProvisionalInfo.ContainsKey("rate"))
            {
                var symbol = info.GetDictionaryValue<string>("symbol");
                var rate = info.GetDictionaryValue<double>("rate");

                if (!CurrencyExchanger.Instance.ExistsCurrency(symbol)) //REVISAR!
                {
                    CurrencyExchanger.Instance.AddCurrency(symbol, rate);
                    info.ComunicationChannel.SendMessage(request.User, "¡Moneda agregada con éxito! 🙌");
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Esta moneda ya existe, pero puedes crear una nueva con el comando /agregarmoneda. 😉");
                }
                info.ClearOperation();
            }
        }
    }
}