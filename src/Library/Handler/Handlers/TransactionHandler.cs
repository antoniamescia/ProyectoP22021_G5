using System;

namespace Library
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/
    /// <summary>
    /// Handler para realizar una transacción.
    /// </summary>
    public class TransactionHandler : AbstractHandler<UserMessage>
    {
        public TransactionHandler(TransactionCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);

            if (!data.Temp.ContainsKey("type"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && (index == 1 || index == 2))
                {
                    data.Temp.Add("type", index);
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese la cuenta en la cual desea realizar la transacción:\n" + data.User.DisplayAccounts());
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Debe ingresar un valor correspondiente al índice del tipo.");
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese el tipo de transacción:\n1 - Ingreso\n2 - Gasto");
                }
            }
            else if (!data.Temp.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= data.User.Accounts.Count)
                {
                    data.Temp.Add("account", data.User.Accounts[index - 1]);
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese la moneda en la cual desea realizar la transacción:\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Debe ingresar un valor correspondiente al índice de la cuenta.");
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese la cuenta en la cual desea realizar la transacción:\n" + data.User.DisplayAccounts());
                }


            }
            else if (!data.Temp.ContainsKey("currency"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    data.Temp.Add("currency", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese el monto de la transacción:");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Debe ingresar un valor correspondiente al índice de la moneda.");
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese la moneda en la cual desea realizar la transacción:\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                }


            }
            else if (!data.Temp.ContainsKey("amount"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0)
                {
                    amount = data.GetDictionaryValue<int>("type") == 1 ? amount : -amount;
                    data.Temp.Add("amount", amount);

                    if (data.GetDictionaryValue<int>("type") == 1)
                    {
                        data.ComunicationChannel.SendMessage(request.User, "Ingrese una descripción de la transacción:");
                    }
                    else
                    {
                        data.ComunicationChannel.SendMessage(request.User, "Seleccione el rubro del gasto:\n" + data.User.DisplayExpenseCategories());
                    }
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Debe ingresar un valor numérico mayor a 0.");
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese el monto de la transacción:");
                }


            }
            else if (!data.Temp.ContainsKey("description"))
            {
                if (data.GetDictionaryValue<int>("type") == 2)
                {
                    int index;
                    if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= data.User.ExpenseCategories.Count)
                    {
                        data.Temp.Add("description", data.User.ExpenseCategories[index - 1]);
                    }
                    else
                    {
                        data.ComunicationChannel.SendMessage(request.User, "Debe ingresar un valor correspondiente al índice del rubro");
                        data.ComunicationChannel.SendMessage(request.User, "Seleccione el rubro del gasto:\n" + data.User.DisplayExpenseCategories());
                        return;
                    }

                }
                else if (data.GetDictionaryValue<int>("type") == 1)
                {
                    data.Temp.Add("description", request.MessageText);
                }
            }



            if (data.Temp.ContainsKey("type") && data.Temp.ContainsKey("account") && data.Temp.ContainsKey("currency") && data.Temp.ContainsKey("amount") && data.Temp.ContainsKey("description"))
            {
                var type = data.GetDictionaryValue<int>("type");
                var account = data.GetDictionaryValue<Account>("account");
                var currency = data.GetDictionaryValue<Currency>("currency");
                var amount = data.GetDictionaryValue<double>("amount");
                var description = data.GetDictionaryValue<string>("description");

                account.Transfer(currency, amount, description);

                data.ComunicationChannel.SendMessage(request.User, "Se ha realizado una transacción.");

                if (account.MinGoal.ObjectiveAmount > account.Amount)
                {
                    data.ComunicationChannel.SendMessage(request.User, "Ha sobrepasado su objetivo mínimo. Debe ahorrar más.");
                }
                else if (account.MaxGoal.ObjectiveAmount < account.Amount)
                {
                    data.ComunicationChannel.SendMessage(request.User, "Ha superado su objetivo máximo. Empiece a gastar.");
                }

                data.ClearOperation();
                return;
            }
        }
    }
}
