using System;

namespace BankerBot
{
    public class ChangeAccountObjectiveHandler : AbstractHandler<IMessage>
    {
        public ChangeAccountObjectiveHandler(ChangeAccountObjectiveCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);

            if (!data.ProvisionalInfo.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= data.User.Accounts.Count)
                {
                    data.ProvisionalInfo.Add("account", data.User.Accounts[index - 1]);
                    data.Channel.SendMessage(request.UserID, "Ingrese un nuevo objetivo de ahorro máximo:");
                }
                else
                {
                    data.Channel.SendMessage(request.UserID, "¿Puedes seleccionar el número correspondiente? 😊");
                    data.Channel.SendMessage(request.UserID, "¿De qué cuenta deseas cambiar el objetivo de ahorro?:\n" + data.User.DisplayAccounts());
                }
                return;
            }
            else if (!data.ProvisionalInfo.ContainsKey("maxObjective"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 1)
                {
                    data.ProvisionalInfo.Add("maxObjective", amount);
                    data.Channel.SendMessage(request.UserID, "Ingrese un nuevo objetivo de ahorro mínimo:");
                }
                else
                {
                    data.Channel.SendMessage(request.UserID, "¡El valor debe ser mayor a 0!");
                    data.Channel.SendMessage(request.UserID, "Ingrese un nuevo objetivo de ahorro máximo:");
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
                    data.Channel.SendMessage(request.UserID, "¡El valor debe ser mayor a 0!");
                    data.Channel.SendMessage(request.UserID, "Ingrese un nuevo objetivo de ahorro mínimo:");
                }
            }

            if (data.ProvisionalInfo.ContainsKey("maxObjective") && data.ProvisionalInfo.ContainsKey("minObjective"))
            {
                var account = data.GetDictionaryValue<Account>("account");
                var maxObjective = data.GetDictionaryValue<double>("maxObjective");
                var minObjective = data.GetDictionaryValue<double>("minObjective");

                account.ChangeSavingsGoal(maxObjective, minObjective);
                data.Channel.SendMessage(request.UserID, "¡Objetivos cambiados con éxito! 👏🏼");

                data.ClearOperation();
            }
        }
    }
}