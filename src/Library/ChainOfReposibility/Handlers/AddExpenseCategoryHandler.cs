using System;

namespace BankerBot
{
    public class AddExpenseCategoryHandler : AbstractHandler<IMessage>
    {
        /// <summary>
        /// Handler que se encargará de agregar nuevas categorías de gasto.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        
        /*
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método HandleRequest.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa el método polimórfico handleRequest.
        Cumple con el patrón Chain of Responsibility.
        */

        public AddExpenseCategoryHandler(AddExpenseCategoryCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);

            if (request.MessageText != string.Empty)
            {
                if (!data.User.ContainsExpenseCategory(request.MessageText))
                {
                    data.User.AddExpenseCategory(request.MessageText);
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Se ha agregado una nueva categoría de gasto con éxito! 🙌");
                    data.ClearOperation();
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Atención! Ya existe una categoría de gasto con este nombre.");
                    data.ComunicationChannel.SendMessage(request.UserID, "Ingrese una nueva categoría de gasto: 💸");
                }
            }
            else
            {
                data.ComunicationChannel.SendMessage(request.UserID, "Debes ingresar una nueva categoría de gasto.");
                data.ComunicationChannel.SendMessage(request.UserID, "Ingrese una nueva categoría de gasto: 💸");
            }
        }
    }
}