using System;
using System.Collections.Generic;
using System.Text;


namespace BankerBot
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/

    /// <summary>
    /// Handler para crear la cuenta.
    /// </summary>
    public class CreateAccountHandler : AbstractHandler<IMessage>
    {
        public CreateAccountHandler(ICondition<IMessage> condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {

            UserInfo data = Session.Instance.GetChatInfo(request.UserID);

            if (!data.ProvisionalInfo.ContainsKey("type"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index > 0 && index <= Enum.GetNames(typeof(Type)).Length)
                {
                    data.ProvisionalInfo.Add("type", (Type)index - 1);
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingresa el nombre de la nueva cuenta.");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingresa el índice, por favor.");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Qué tipo de cuenta es?\n" + Account.DisplayAccountType());
                }
            }
            else if (!data.ProvisionalInfo.ContainsKey("name"))
            {
                if (!data.User.AccountExists(request.MessageText))
                {
                    data.ProvisionalInfo.Add("name", request.MessageText);
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Qué moneda tiene la cuenta? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Ya existe una cuenta con ese nombre! Vuelve a ingresar un nombre de cuenta, por favor.");
                }
            }
            else if (!data.ProvisionalInfo.ContainsKey("currency"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    data.ProvisionalInfo.Add("currency", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Cuál es el balance inicial de la cuenta?");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingresa el índice, por favor.");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Qué moneda tiene la cuenta? 🪙:\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
            }
            else if (!data.ProvisionalInfo.ContainsKey("amount"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0)
                {
                    data.ProvisionalInfo.Add("amount", amount);
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Cuál es el objetivo máximo de ahorro de la cuenta? 💸");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Ingresa otro valor!");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Cuál es el balance inicial de la cuenta?");
                }
            }
            else if (!data.ProvisionalInfo.ContainsKey("maxObjective"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 1)
                {
                    data.ProvisionalInfo.Add("maxObjective", amount);
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Cuál es el objetivo mínimo de ahorro de la cuenta? ");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Ingresa otro valor!");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Cuál es el objetivo máximo de ahorro de la cuenta? 💸");
                }
            }
            else if (!data.ProvisionalInfo.ContainsKey("minObjective"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0 && amount < data.GetDictionaryValue<double>("maxObjective"))
                {
                    data.ProvisionalInfo.Add("minObjective", amount);
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Ingresa otro valor!");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Cuál es el objetivo mínimo de ahorro de la cuenta?");
                }
            }

            if (data.ProvisionalInfo.ContainsKey("type") && data.ProvisionalInfo.ContainsKey("name") && data.ProvisionalInfo.ContainsKey("currency") && data.ProvisionalInfo.ContainsKey("amount") && data.ProvisionalInfo.ContainsKey("maxObjective") && data.ProvisionalInfo.ContainsKey("minObjective"))
            {
                Type type = data.GetDictionaryValue<Type>("type");
                string name = data.GetDictionaryValue<string>("name");
                Currency currency = data.GetDictionaryValue<Currency>("currency");
                double amount = data.GetDictionaryValue<double>("amount");
                double maxObjective = data.GetDictionaryValue<double>("maxObjective");
                double minObjective = data.GetDictionaryValue<double>("minObjective");

                Account account = data.User.AddAccount(type, name, currency, amount, new SavingsGoal(maxObjective, minObjective));

                if (account != null)
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Wohoo! ¡Cuenta creada con éxito! 🙌");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Ha ocurrido un error! 🥲");
                }

                data.ClearOperation();
            }
        }
    }
}