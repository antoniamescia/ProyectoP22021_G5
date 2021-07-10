using System;
using System.Collections.Generic;
using System.Text;


namespace Bankbot
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

            Data data = Session.Instance.GetChat(request.Id);

            if (!data.Temp.ContainsKey("type"))
            {
                int index;
                if (Int32.TryParse(request.Text, out index) && index > 0 && index > 0 && index <= Enum.GetNames(typeof(AccountType)).Length)
                {
                    data.Temp.Add("type", (AccountType)index - 1);
                    data.Channel.SendMessage(request.Id, "Ingresa el nombre de la nueva cuenta.");
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "Ingresa el índice, por favor.");
                    data.Channel.SendMessage(request.Id, "¿Qué tipo de cuenta es?\n" + Account.ShowAccountType());
                }
            }
            else if (!data.Temp.ContainsKey("name"))
            {
                if (!data.User.AccountNameExists(request.Text))
                {
                    data.Temp.Add("name", request.Text);
                    data.Channel.SendMessage(request.Id, "¿Qué moneda tiene la cuenta? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "¡Ya existe una cuenta con ese nombre! Vuelve a ingresar un nombre de cuenta, por favor.");
                }
            }
            else if (!data.Temp.ContainsKey("currency"))
            {
                int index;
                if (Int32.TryParse(request.Text, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    data.Temp.Add("currency", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    data.Channel.SendMessage(request.Id, "¿Cuál es el balance inicial de la cuenta?");
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "Ingresa el índice, por favor.");
                    data.Channel.SendMessage(request.Id, "¿Qué moneda tiene la cuenta? 🪙:\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
            }
            else if (!data.Temp.ContainsKey("amount"))
            {
                double amount;
                if (double.TryParse(request.Text, out amount) && amount > 0)
                {
                    data.Temp.Add("amount", amount);
                    data.Channel.SendMessage(request.Id, "¿Cuál es el objetivo máximo de ahorro de la cuenta? 💸");
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "¡Ingresa otro valor!");
                    data.Channel.SendMessage(request.Id, "¿Cuál es el balance inicial de la cuenta?");
                }
            }
            else if (!data.Temp.ContainsKey("maxObjective"))
            {
                double amount;
                if (double.TryParse(request.Text, out amount) && amount > 1)
                {
                    data.Temp.Add("maxObjective", amount);
                    data.Channel.SendMessage(request.Id, "¿Cuál es el objetivo mínimo de ahorro de la cuenta? ");
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "¡Ingresa otro valor!");
                    data.Channel.SendMessage(request.Id, "¿Cuál es el objetivo máximo de ahorro de la cuenta? 💸");
                }
            }
            else if (!data.Temp.ContainsKey("minObjective"))
            {
                double amount;
                if (double.TryParse(request.Text, out amount) && amount > 0 && amount < data.GetDictionaryValue<double>("maxObjective"))
                {
                    data.Temp.Add("minObjective", amount);
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "¡Ingresa otro valor!");
                    data.Channel.SendMessage(request.Id, "¿Cuál es el objetivo mínimo de ahorro de la cuenta?");
                }
            }

            if (data.Temp.ContainsKey("type") && data.Temp.ContainsKey("name") && data.Temp.ContainsKey("currency") && data.Temp.ContainsKey("amount") && data.Temp.ContainsKey("maxObjective") && data.Temp.ContainsKey("minObjective"))
            {
                var type = data.GetDictionaryValue<AccountType>("type");
                var name = data.GetDictionaryValue<string>("name");
                var currency = data.GetDictionaryValue<Currency>("currency");
                var amount = data.GetDictionaryValue<double>("amount");
                var maxObjective = data.GetDictionaryValue<double>("maxObjective");
                var minObjective = data.GetDictionaryValue<double>("minObjective");

                var account = data.User.AddAccount(type, name, currency, amount, new Objective(maxObjective, minObjective));

                if (account != null)
                {
                    data.Channel.SendMessage(request.Id, "¡Wohoo! ¡Cuenta creada con éxito! 🙌");
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "¡Ha ocurrido un error! 🥲");
                }

                data.ClearOperation();
            }
        }
    }
}