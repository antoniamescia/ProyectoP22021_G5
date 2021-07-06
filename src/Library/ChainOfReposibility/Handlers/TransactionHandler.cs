using System;

namespace Library
{
    /// <summary>
    /// Este Handler se encarga de llevar a cabo las transacciones, tanto ingresos como egresos. 
    /// </summary>
       public class TransactionHandler : AbstractHandler<UserMessage>
    {
        public TransactionHandler(TransactionCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.User);

            if (!data.ProvisionalInfo.ContainsKey("type"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && (index == 1 || index == 2))
                {
                    data.ProvisionalInfo.Add("type", index);
                    data.ComunicationChannel.SendMessage(request.User, "¿En qué cuenta quieres realizar la transacción?:🤔\n" + data.User.DisplayAccounts());
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "VER QUE PONER ACÁ.");
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese el tipo de transacción:\n1 - Ingreso\n2 - Egreso");
                }
            }
            else if (!data.ProvisionalInfo.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= data.User.Accounts.Count)
                {
                    data.ProvisionalInfo.Add("account", data.User.Accounts[index - 1]);
                    data.ComunicationChannel.SendMessage(request.User, "¿En qué moneda quieres realizar la transacción?🪙:\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "VER QUE PONER ACÁ.");
                    data.ComunicationChannel.SendMessage(request.User, "¿En qué cuenta quieres realizar la transacción?:🤔\n" + data.User.DisplayAccounts());
                }


            }
            else if (!data.ProvisionalInfo.ContainsKey("currency"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    data.ProvisionalInfo.Add("currency", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese el monto de la transacción:");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "//");
                    data.ComunicationChannel.SendMessage(request.User, "¿En qué moneda quieres realizar la transacción?🪙:\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                }


            }
            else if (!data.ProvisionalInfo.ContainsKey("amount"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0)
                {
                    amount = data.GetDictionaryValue<int>("type") == 1 ? amount : -amount;
                    data.ProvisionalInfo.Add("amount", amount);

                    if (data.GetDictionaryValue<int>("type") == 1)
                    {
                        data.ComunicationChannel.SendMessage(request.User, "Describe la transacción:");
                    }
                    else
                    {
                        data.ComunicationChannel.SendMessage(request.User, "¿Qué tipo de gasto es?:\n" + data.User.DisplayExpenseCategories());
                    }
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese un valor mayor a 0. ⚠️");
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese el monto de la transacción:");
                }


            }
            else if (!data.ProvisionalInfo.ContainsKey("description"))
            {
                if (data.GetDictionaryValue<int>("type") == 2)
                {
                    int index;
                    if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= data.User.ExpenseCategories.Count)
                    {
                        data.ProvisionalInfo.Add("description", data.User.ExpenseCategories[index - 1]);
                    }
                    else
                    {
                        data.ComunicationChannel.SendMessage(request.User, "//");
                        data.ComunicationChannel.SendMessage(request.User, "¿Qué tipo de gasto es?:\n" + data.User.DisplayExpenseCategories());
                        return;
                    }

                }
                else if (data.GetDictionaryValue<int>("type") == 1)
                {
                    data.ProvisionalInfo.Add("description", request.MessageText);
                }
            }



            if (data.ProvisionalInfo.ContainsKey("type") && data.ProvisionalInfo.ContainsKey("account") && data.ProvisionalInfo.ContainsKey("currency") && data.ProvisionalInfo.ContainsKey("amount") && data.ProvisionalInfo.ContainsKey("description"))
            {
                var type = data.GetDictionaryValue<int>("type");
                var account = data.GetDictionaryValue<Account>("account");
                var currency = data.GetDictionaryValue<Currency>("currency");
                var amount = data.GetDictionaryValue<double>("amount");
                var description = data.GetDictionaryValue<string>("description");

                account.Transfer(currency, amount, description);

                data.ComunicationChannel.SendMessage(request.User, "¡Transacción exitosa! 🙌");

                // AGREGAR LO DE LAS ALERTAS! 

                data.ClearOperation();
                return;
            }
        }
    }
}
