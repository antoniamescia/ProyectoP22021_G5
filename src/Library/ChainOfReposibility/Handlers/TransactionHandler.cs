using System;

namespace Bankbot
{

    public class TransactionHandler : AbstractHandler<IMessage>
    {
        public TransactionHandler(TransactionCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);

            if (!data.Temp.ContainsKey("type"))
            {
                int index;
                if (Int32.TryParse(request.Text, out index) && index > 0 && (index == 1 || index == 2))
                {
                    data.Temp.Add("type", index);
                    data.Channel.SendMessage(request.Id, "¿Desde qué cuenta quieres realizar la transacción?\n" + data.User.ShowAccountList());
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "Ingresa el índice, por favor.");
                    data.Channel.SendMessage(request.Id, "¿Qué tipo de transacción queires realizar? \n1 - Ingreso\n2 - Egreso");
                }
            }
            else if (!data.Temp.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.Text, out index) && index > 0 && index <= data.User.Accounts.Count)
                {
                    data.Temp.Add("account", data.User.Accounts[index - 1]);
                    data.Channel.SendMessage(request.Id, "¿En qué moneda quieres realizar la transacción? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "Ingresa el índice, por favor.");
                    data.Channel.SendMessage(request.Id, "¿Desde qué cuenta quieres realizar la transacción?\n" + data.User.ShowAccountList());
                }


            }
            else if (!data.Temp.ContainsKey("currency"))
            {
                int index;
                if (Int32.TryParse(request.Text, out index) && index > 0 && index <= CurrencyExchanger.Instance.CurrencyList.Count)
                {
                    data.Temp.Add("currency", CurrencyExchanger.Instance.CurrencyList[index - 1]);
                    data.Channel.SendMessage(request.Id, "¿Cuál es el monto de la transacción?");
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "Ingresa el índice, por favor.");
                    data.Channel.SendMessage(request.Id, "¿En qué moneda quieres realizar la transacción? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                }


            }
            else if (!data.Temp.ContainsKey("amount"))
            {
                double amount;
                if (double.TryParse(request.Text, out amount) && amount > 0)
                {
                    amount = data.GetDictionaryValue<int>("type") == 1 ? amount : -amount;
                    data.Temp.Add("amount", amount);

                    if (data.GetDictionaryValue<int>("type") == 1)
                    {
                        data.Channel.SendMessage(request.Id, "Describe la transacción:");
                    }
                    else
                    {
                        data.Channel.SendMessage(request.Id, "¿A qué corresponde este egreso?\n" + data.User.ShowItemList());
                    }
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "¡Ingresa un valor mayor a 0!");
                    data.Channel.SendMessage(request.Id, "¿Cuál es el monto de la transacción?");
                }


            }
            else if (!data.Temp.ContainsKey("description"))
            {
                if (data.GetDictionaryValue<int>("type") == 2)
                {
                    int index;
                    if (Int32.TryParse(request.Text, out index) && index > 0 && index <= data.User.ExpenseCategories.Count)
                    {
                        data.Temp.Add("description", data.User.ExpenseCategories[index - 1]);
                    }
                    else
                    {
                        data.Channel.SendMessage(request.Id, "Ingresa el índice, por favor.");
                        data.Channel.SendMessage(request.Id, "¿A qué corresponde este egreso?\n" + data.User.ShowItemList());
                        return;
                    }

                }
                else if (data.GetDictionaryValue<int>("type") == 1)
                {
                    data.Temp.Add("description", request.Text);
                }
            }



            if (data.Temp.ContainsKey("type") && data.Temp.ContainsKey("account") && data.Temp.ContainsKey("currency") && data.Temp.ContainsKey("amount") && data.Temp.ContainsKey("description"))
            {
                var type = data.GetDictionaryValue<int>("type");
                var account = data.GetDictionaryValue<Account>("account");
                var currency = data.GetDictionaryValue<Currency>("currency");
                var amount = data.GetDictionaryValue<double>("amount");
                var description = data.GetDictionaryValue<string>("description");

                account.AddTransaction(currency, amount, description);

                data.Channel.SendMessage(request.Id, "¡Transacción realizada con éxito! 🙌");

                IAlert alert1 = new MaxSavingsGoalAlert();
                IAlert alert2 = new MaxSavingsGoalReachedAlert();
                IAlert alert3 = new MinSavingsGoalAlert();
                IAlert alert4 = new MinSavingsGoalReachedAlert();

                data.Channel.SendMessage(request.Id, "ALERTA ⚠️:\n"+ alert1.SendAlert(account) + "\n" + alert2.SendAlert(account) + "\n" + alert3.SendAlert(account) + "\n" + alert4.SendAlert(account));

                data.ClearOperation();
                return;
            }
        }
    }
}
