using System;

namespace BankerBot
{
    public class ChangeSavingsGoalHandler : AbstractHandler<IMessage>
    {
        /// <summary>
        /// Handler que se encargará de cambiar el objetivo de ahorro de una cuenta particular. 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        
        /*
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método HandleRequest.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa el método polimórfico handleRequest.
        Cumple con el patrón Chain of Responsibility.
        */
        
        public ChangeSavingsGoalHandler(ChangeSavingsGoalCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);

            if (!data.ProvisionalInfo.ContainsKey("account"))
            {
                int index;
                if (Int32.TryParse(request.MessageText, out index) && index > 0 && index <= data.User.Accounts.Count)
                {
                    data.ProvisionalInfo.Add("account", data.User.Accounts[index - 1]);
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingrese un nuevo objetivo de ahorro máximo:");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Puedes seleccionar el número correspondiente? 😊");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿De qué cuenta deseas cambiar el objetivo de ahorro?:\n" + data.User.DisplayAccounts());
                }
                return;
            }
            else if (!data.ProvisionalInfo.ContainsKey("maxObjective"))
            {
                double amount;
                if (double.TryParse(request.MessageText, out amount) && amount > 1)
                {
                    data.ProvisionalInfo.Add("maxObjective", amount);
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingrese un nuevo objetivo de ahorro mínimo:");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "¡El valor debe ser mayor a 0!");
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingrese un nuevo objetivo de ahorro máximo:");
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
                    data.ComunicationChannel.SendMessage(request.UserID, "¡El valor debe ser mayor a 0!");
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingrese un nuevo objetivo de ahorro mínimo:");
                }
            }

            if (data.ProvisionalInfo.ContainsKey("maxObjective") && data.ProvisionalInfo.ContainsKey("minObjective"))
            {
                Account account = data.GetDictionaryValue<Account>("account");
                double maxObjective = data.GetDictionaryValue<double>("maxObjective");
                double minObjective = data.GetDictionaryValue<double>("minObjective");

                account.ChangeSavingsGoal(maxObjective, minObjective);
                data.ComunicationChannel.SendMessage(request.UserID, "¡Objetivos cambiados con éxito! 👏🏼");

                data.ClearOperation();
            }
        }
    }
}