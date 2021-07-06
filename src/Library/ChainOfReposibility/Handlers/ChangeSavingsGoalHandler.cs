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
            UserInfo info = Session.Instance.GetChatInfo(request.User);

            if (!info.ProvisionalInfo.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= info.User.Accounts.Count)
                {
                    info.ProvisionalInfo.Add("account", info.User.Accounts[index - 1]);
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese un nuevo objetivo de ahorro máximo: 💰");
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "//"); //REVISAR!
                    info.ComunicationChannel.SendMessage(request.User, "¿De qué cuenta deseas cambiar el objetivo?:\n" + info.User.DisplayAccounts());
                }
                return;
            }
            else if (!info.ProvisionalInfo.ContainsKey("maxSavingsGoal")) 
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 1)
                {
                    info.ProvisionalInfo.Add("maxSavingsGoal", amount);
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese un nuevo objetivo de ahorro mínimo: 💰");
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "//"); //REVISAR!
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese un nuevo objetivo de ahorro máximo: 💰");
                }
            }
            else if (!info.ProvisionalInfo.ContainsKey("minSavingsGoal"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 0 && amount < info.GetDictionaryValue<double>("maxSavingsGoal"))
                {
                    info.ProvisionalInfo.Add("minSavingsGoal", amount);
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "//"); //REVISAR!
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese un nuevo objetivo de ahorro mínimo: 💰");
                }
            }

            if (info.ProvisionalInfo.ContainsKey("maxSavingsGoal") && info.ProvisionalInfo.ContainsKey("minSavingsGoal"))
            {
                var account = info.GetDictionaryValue<Account>("account");
                var maxSavingsGoal = info.GetDictionaryValue<double>("maxSavingsGoal");
                var minSavingsGoal = info.GetDictionaryValue<double>("minSavingsGoal");

                //account.ChangeMaxGoal(maxSavingsGoal, minSavingsGoal);
                info.ComunicationChannel.SendMessage(request.User, "¡Objetivos actualizados con éxito!");

                info.ClearOperation();
            }
        }
    }
}