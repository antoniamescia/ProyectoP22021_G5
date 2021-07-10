using System;

namespace BankerBot
{
    public class AddExpenseCategoryHandler : AbstractHandler<IMessage>
    {
        public AddExpenseCategoryHandler(AddExpenseCategoryCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);

            if (request.MessageText != string.Empty)
            {
                if (!data.User.ContainsExpenseCategory(request.MessageText))
                {
                    data.User.ExpenseCategories.Add(request.MessageText);
                    data.Channel.SendMessage(request.UserID, "¡Se ha agregado una nueva categoría de gasto con éxito! 🙌");
                    data.ClearOperation();
                }
                else
                {
                    data.Channel.SendMessage(request.UserID, "¡Atención! Ya existe una categoría de gasto con este nombre.");
                    data.Channel.SendMessage(request.UserID, "Ingrese una nueva categoría de gasto: 💸");
                }
            }
            else
            {
                data.Channel.SendMessage(request.UserID, "Debes ingresar una nueva categoría de gasto.");
                data.Channel.SendMessage(request.UserID, "Ingrese una nueva categoría de gasto: 💸");
            }
        }
    }
}