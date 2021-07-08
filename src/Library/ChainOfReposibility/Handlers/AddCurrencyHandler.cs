// using System;

// namespace Bankbot
// {
//     public class AddCurrencyHandler : AbstractHandler<IMessage>
//     {
//         public AddCurrencyHandler(AddCurrencyCondition condition) : base(condition)
//         {
//         }

//         protected override void handleRequest(IMessage request)
//         {
//             Data data = Session.Instance.GetChat(request.Id);

//             if (!data.Temp.ContainsKey("code"))
//             {
//                 data.Temp.Add("code", request.Text);
//                 data.Channel.SendMessage(request.Id, "Ingresa el símbolo de la nueva moneda: 💲");
//             }
//             else if (!data.Temp.ContainsKey("symbol"))
//             {
//                 data.Temp.Add("symbol", request.Text);
//                 data.Channel.SendMessage(request.Id, "Ingrese la taza de cambio basada en pesos uruguayos: 🇺🇾");
//             }
//             else if (!data.Temp.ContainsKey("rate"))
//             {
//                 double rate;
//                 if (double.TryParse(request.Text, out rate) && rate > 0)
//                 {
//                     data.Temp.Add("rate", rate);
//                 }
//                 else
//                 {
//                     data.Channel.SendMessage(request.Id, "¡Atención! Ingrese un número mayor que 0.");
//                     data.Channel.SendMessage(request.Id, "Ingrese la taza de cambio basada en pesos uruguayos: 🇺🇾");
//                 }
//             }

//             if (data.Temp.ContainsKey("iso") && data.Temp.ContainsKey("symbol") && data.Temp.ContainsKey("rate"))
//             {
//                 var iso = data.GetDictionaryValue<string>("iso");
//                 var symbol = data.GetDictionaryValue<string>("symbol");
//                 var rate = data.GetDictionaryValue<double>("rate");

//                 if (!CurrencyExchanger.Instance.CurrencyExists(symbol))
//                 {
//                     CurrencyExchanger.Instance.AddCurrency(iso, symbol, rate);
//                     data.Channel.SendMessage(request.Id, "¡Moneda agregada con éxito! 🙌");
//                 }
//                 else
//                 {
//                     data.Channel.SendMessage(request.Id, "¡Esta moneda ya existe! Puedes crear una nueva con el comando /AgregarMoneda.");
//                 }
//                 data.ClearOperation();
//             }
//         }
//     }
// }