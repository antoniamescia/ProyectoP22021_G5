using System;

namespace Bankbot
{
    public class AddExpenseCategoryHandler : AbstractHandler<IMessage>
    {
        public AddExpenseCategoryHandler(AddExpenseCategoryCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);

            if (request.Text != string.Empty)
            {
                if (!data.User.ContainsItem(request.Text))
                {
                    data.User.ExpenseCategories.Add(request.Text);
                    data.Channel.SendMessage(request.Id, "¡Se ha agregado una nueva categoría de gasto con éxito! 🙌");
                    data.ClearOperation();
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "¡Atención! Ya existe una categoría de gasto con este nombre.");
                    data.Channel.SendMessage(request.Id, "Ingrese una nueva categoría de gasto: 💸");
                }
            }
            else
            {
                data.Channel.SendMessage(request.Id, "Debes ingresar una nueva categoría de gasto.");
                data.Channel.SendMessage(request.Id, "Ingrese una nueva categoría de gasto: 💸");
            }
        }
    }
}