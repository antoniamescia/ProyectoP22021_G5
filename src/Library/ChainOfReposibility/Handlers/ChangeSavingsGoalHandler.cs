using System;

namespace Library
{
    public class ChangeSavingsGoalHandler : AbstractHandler<UserMessage>
    {
        public ChangeSavingsGoalHandler(ChangeSavingsGoalCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.User);

            if (!data.ProvisionalInfo.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= data.User.Accounts.Count)
                {
                    data.ProvisionalInfo.Add("account", data.User.Accounts[index - 1]);
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese un nuevo objetivo de ahorro máximo: 💰");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "//"); //REVISAR!
                    data.ComunicationChannel.SendMessage(request.User, "¿De qué cuenta deseas cambiar el objetivo?:\n" + data.User.DisplayAccounts());
                }
                return;
            }
            else if (!data.ProvisionalInfo.ContainsKey("maxSavingsGoal")) 
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 1)
                {
                    data.ProvisionalInfo.Add("maxSavingsGoal", amount);
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese un nuevo objetivo de ahorro mínimo: 💰");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "//"); //REVISAR!
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese un nuevo objetivo de ahorro máximo: 💰");
                }
            }
            else if (!data.ProvisionalInfo.ContainsKey("minSavingsGoal"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0 && amount < data.GetDictionaryValue<double>("maxSavingsGoal"))
                {
                    data.ProvisionalInfo.Add("minSavingsGoal", amount);
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "//"); //REVISAR!
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese un nuevo objetivo de ahorro mínimo: 💰");
                }
            }

            if (data.ProvisionalInfo.ContainsKey("maxSavingsGoal") && data.ProvisionalInfo.ContainsKey("minSavingsGoal"))
            {
                var account = data.GetDictionaryValue<Account>("account");
                var maxSavingsGoal = data.GetDictionaryValue<double>("maxSavingsGoal");
                var minSavingsGoal = data.GetDictionaryValue<double>("minSavingsGoal");

                //account.ChangeMaxGoal(maxSavingsGoal, minSavingsGoal);
                data.ComunicationChannel.SendMessage(request.User, "¡Objetivos actualizados con éxito!");

                data.ClearOperation();
            }
        }
    }
}