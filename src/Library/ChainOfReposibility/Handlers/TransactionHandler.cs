using System;

namespace BankerBot
{

    public class TransactionHandler : AbstractHandler<IMessage>
    {
        public TransactionHandler(TransactionCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);

            if (!data.ProvisionalInfo.ContainsKey("type"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && (index == 1 || index == 2))
                {
                    data.ProvisionalInfo.Add("type", index);
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Desde qué cuenta quieres realizar la transacción?\n" + data.User.DisplayAccounts());
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingresa el índice, por favor.");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Qué tipo de transacción queires realizar? \n1 - Ingreso\n2 - Egreso");
                }
            }
            else if (!data.ProvisionalInfo.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= data.User.Accounts.Count)
                {
                    data.ProvisionalInfo.Add("account", data.User.Accounts[index - 1]);
                    data.ComunicationChannel.SendMessage(request.UserID, "¿En qué moneda quieres realizar la transacción? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingresa el índice, por favor.");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Desde qué cuenta quieres realizar la transacción?\n" + data.User.DisplayAccounts());
                }


            }
            else if (!data.ProvisionalInfo.ContainsKey("currency"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    data.ProvisionalInfo.Add("currency", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Cuál es el monto de la transacción?");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingresa el índice, por favor.");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿En qué moneda quieres realizar la transacción? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
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
                        data.ComunicationChannel.SendMessage(request.UserID, "Describe la transacción:");
                    }
                    else
                    {
                        data.ComunicationChannel.SendMessage(request.UserID, "¿A qué corresponde este egreso?\n" + data.User.DisplayExpenseCategories());
                    }
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Ingresa un valor mayor a 0!");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Cuál es el monto de la transacción?");
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
                        data.ComunicationChannel.SendMessage(request.UserID, "Ingresa el índice, por favor.");
                        data.ComunicationChannel.SendMessage(request.UserID, "¿A qué corresponde este egreso?\n" + data.User.DisplayExpenseCategories());
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

                data.ComunicationChannel.SendMessage(request.UserID, "¡Transacción realizada con éxito! 🙌");

                IAlert alert1 = new MaxSavingsGoalAlert();
                IAlert alert2 = new MaxSavingsGoalReachedAlert();
                IAlert alert3 = new MinSavingsGoalAlert();
                IAlert alert4 = new MinSavingsGoalReachedAlert();

                if (alert1.SendAlert(account) != null)
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "ALERTA1 ⚠️:\n"+ alert1.SendAlert(account));
                }
                else if (alert2.SendAlert(account) != null)
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "ALERTA2 ⚠️:\n"+ alert2.SendAlert(account));
                }
                else if (alert3.SendAlert(account) != null)
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "ALERTA3 ⚠️:\n"+ alert3.SendAlert(account));
                }
                else if (alert4.SendAlert(account) != null)
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "ALERTA4 ⚠️:\n"+ alert4.SendAlert(account));
                }


                data.ClearOperation();
                return;
            }
        }
    }
}
