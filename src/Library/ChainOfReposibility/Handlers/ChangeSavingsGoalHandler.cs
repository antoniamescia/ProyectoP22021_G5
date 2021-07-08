using System;

namespace Bankbot
{
    public class ChangeAccountObjectiveHandler : AbstractHandler<IMessage>
    {
        public ChangeAccountObjectiveHandler(ChangeAccountObjectiveCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);

            if (!data.Temp.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.Text, out index) && index > 0 && index <= data.User.Accounts.Count)
                {
                    data.Temp.Add("account", data.User.Accounts[index - 1]);
                    data.Channel.SendMessage(request.Id, "Ingrese un nuevo objetivo de ahorro máximo:");
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "¿Puedes seleccionar el número correspondiente? 😊");
                    data.Channel.SendMessage(request.Id, "¿De qué cuenta deseas cambiar el objetivo de ahorro?:\n" + data.User.ShowAccountList());
                }
                return;
            }
            else if (!data.Temp.ContainsKey("maxObjective"))
            {
                double amount;
                if (double.TryParse(request.Text, out amount) && amount > 1)
                {
                    data.Temp.Add("maxObjective", amount);
                    data.Channel.SendMessage(request.Id, "Ingrese un nuevo objetivo de ahorro mínimo:");
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "¡El valor debe ser mayor a 0!");
                    data.Channel.SendMessage(request.Id, "Ingrese un nuevo objetivo de ahorro máximo:");
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
                    data.Channel.SendMessage(request.Id, "¡El valor debe ser mayor a 0!");
                    data.Channel.SendMessage(request.Id, "Ingrese un nuevo objetivo de ahorro mínimo:");
                }
            }

            if (data.Temp.ContainsKey("maxObjective") && data.Temp.ContainsKey("minObjective"))
            {
                var account = data.GetDictionaryValue<Account>("account");
                var maxObjective = data.GetDictionaryValue<double>("maxObjective");
                var minObjective = data.GetDictionaryValue<double>("minObjective");

                account.ChangeObjective(maxObjective, minObjective);
                data.Channel.SendMessage(request.Id, "¡Objetivos cambiados con éxito!");

                data.ClearOperation();
            }
        }
    }
}