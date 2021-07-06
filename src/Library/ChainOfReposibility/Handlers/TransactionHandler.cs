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
            UserInfo info = Session.Instance.GetChatInfo(request.User);

            if (!info.ProvisionalInfo.ContainsKey("type"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && (index == 1 || index == 2))
                {
                    info.ProvisionalInfo.Add("type", index);
                    info.ComunicationChannel.SendMessage(request.User, "¿En qué cuenta quieres realizar la transacción?:🤔\n" + info.User.DisplayAccounts());
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "VER QUE PONER ACÁ.");
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese el tipo de transacción:\n1 - Ingreso\n2 - Egreso");
                }
            }
            else if (!info.ProvisionalInfo.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= info.User.Accounts.Count)
                {
                    info.ProvisionalInfo.Add("account", info.User.Accounts[index - 1]);
                    info.ComunicationChannel.SendMessage(request.User, "¿En qué moneda quieres realizar la transacción?🪙:\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "VER QUE PONER ACÁ.");
                    info.ComunicationChannel.SendMessage(request.User, "¿En qué cuenta quieres realizar la transacción?:🤔\n" + info.User.DisplayAccounts());
                }


            }
            else if (!info.ProvisionalInfo.ContainsKey("currency"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    info.ProvisionalInfo.Add("currency", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese el monto de la transacción:");
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "//");
                    info.ComunicationChannel.SendMessage(request.User, "¿En qué moneda quieres realizar la transacción?🪙:\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }


            }
            else if (!info.ProvisionalInfo.ContainsKey("amount"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0)
                {
                    amount = info.GetDictionaryValue<int>("type") == 1 ? amount : -amount;
                    info.ProvisionalInfo.Add("amount", amount);

                    if (info.GetDictionaryValue<int>("type") == 1)
                    {
                        info.ComunicationChannel.SendMessage(request.User, "Describe la transacción:");
                    }
                    else
                    {
                        info.ComunicationChannel.SendMessage(request.User, "¿Qué tipo de gasto es?:\n" + info.User.DisplayExpenseCategories());
                    }
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese un valor mayor a 0. ⚠️");
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese el monto de la transacción:");
                }


            }
            else if (!info.ProvisionalInfo.ContainsKey("description"))
            {
                if (info.GetDictionaryValue<int>("type") == 2)
                {
                    int index;
                    if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= info.User.ExpenseCategories.Count)
                    {
                        info.ProvisionalInfo.Add("description", info.User.ExpenseCategories[index - 1]);
                    }
                    else
                    {
                        info.ComunicationChannel.SendMessage(request.User, "//");
                        info.ComunicationChannel.SendMessage(request.User, "¿Qué tipo de gasto es?:\n" + info.User.DisplayExpenseCategories());
                        return;
                    }

                }
                else if (info.GetDictionaryValue<int>("type") == 1)
                {
                    info.ProvisionalInfo.Add("description", request.MessageText);
                }
            }



            if (info.ProvisionalInfo.ContainsKey("type") && info.ProvisionalInfo.ContainsKey("account") && info.ProvisionalInfo.ContainsKey("currency") && info.ProvisionalInfo.ContainsKey("amount") && info.ProvisionalInfo.ContainsKey("description"))
            {
                var type = info.GetDictionaryValue<int>("type");
                var account = info.GetDictionaryValue<Account>("account");
                var currency = info.GetDictionaryValue<Currency>("currency");
                var amount = info.GetDictionaryValue<double>("amount");
                var description = info.GetDictionaryValue<string>("description");

                account.Transfer(currency, amount, description);

                info.ComunicationChannel.SendMessage(request.User, "¡Transacción exitosa! 🙌");

                // AGREGAR LO DE LAS ALERTAS! 

                info.ClearOperation();
                return;
            }
        }
    }
}
