using System;
using System.Collections.Generic;

namespace Library
{
    public class CreateAccountHandler : AbstractHandler<UserMessage>
    {
        public CreateAccountHandler(CreateAccountCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {

            UserInfo info = Session.Instance.GetChatInfo(request.User);

            if (!info.ProvisionalInfo.ContainsKey("type"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index > 0) //&& index <= Enum.GetNames(typeof(AccountType)).Length)  REVISAR!
                {
                    // info.ProvisionalInfo.Add("type", (AccountType)index - 1);
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese un nombre de cuenta:");
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Debe ingresar el índice correspondiente.");
                    //info.ComunicationChannel.SendMessage(request.User, "Ingrese el tipo de cuenta:\n" + Account.ShowAccountType()); REVISAR
                }
            }
            else if (!info.ProvisionalInfo.ContainsKey("name"))
            {
                if (!info.User.AccountExists(request.MessageText))
                {
                    info.ProvisionalInfo.Add("name", request.MessageText);
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese el tipo de moneda de la cuenta:\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Ya existe una cuenta con este nombre, vuelva a ingresar un nombre de cuenta.");
                }
            }
            else if (!info.ProvisionalInfo.ContainsKey("currency"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    info.ProvisionalInfo.Add("currency", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese el saldo inicial de la cuenta:");
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Debe ingresar el índece correspondiente.");
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese el tipo de moneda de la cuenta:\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
            }
            else if (!info.ProvisionalInfo.ContainsKey("amount"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0)
                {
                    info.ProvisionalInfo.Add("amount", amount);
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese el objetivo máximo de la cuenta:");
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Debe ingresar un valor válido.");
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese el saldo inicial de la cuenta:");
                }
            }
            else if (!info.ProvisionalInfo.ContainsKey("maxObjective"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 1)
                {
                    info.ProvisionalInfo.Add("maxObjective", amount);
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese el objetivo mínimo de la cuenta:");
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Debe ingresar un valor válido.");
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese el objetivo máximo de la cuenta:");
                }
            }
            else if (!info.ProvisionalInfo.ContainsKey("minObjective"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0 && amount < info.GetDictionaryValue<double>("maxObjective"))
                {
                    info.ProvisionalInfo.Add("minObjective", amount);
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Debe ingresar un valor válido.");
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese el objetivo mínimo de la cuenta:");
                }
            }

            if (info.ProvisionalInfo.ContainsKey("type") && info.ProvisionalInfo.ContainsKey("name") && info.ProvisionalInfo.ContainsKey("currency") && info.ProvisionalInfo.ContainsKey("amount") && info.ProvisionalInfo.ContainsKey("maxObjective") && info.ProvisionalInfo.ContainsKey("minObjective"))
            {
                //var type = info.GetDictionaryValue<AccountType>("type");
                var name = info.GetDictionaryValue<string>("name");
                var currency = info.GetDictionaryValue<Currency>("currency");
                var amount = info.GetDictionaryValue<double>("amount");
                var maxSavingsGoal = info.GetDictionaryValue<double>("maxObjective");
                var minSavingsGoal = info.GetDictionaryValue<double>("minObjective");

                // //var account = info.User.AddAccount(name, currency, amount, new SavingsGoal(minSavingsGoal, maxSavingsGoal));

                // // if (account != null)
                // // {
                // //     info.ComunicationChannel.SendMessage(request.User, "Cuenta creada exitosamente.");
                // // }
                // else
                // {
                //     info.ComunicationChannel.SendMessage(request.User, "Ha ocurrido un problema.");
                // }

                info.ClearOperation();
            }
        }
    }
}