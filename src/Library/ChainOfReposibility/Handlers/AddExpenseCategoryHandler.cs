using System;

namespace BankerBot
{
    public class AddExpenseCategoryHandler : AbstractHandler<UserMessage>
    {
        public AddExpenseCategoryHandler(AddExpenseCategoryCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.User);

            if (request.MessageText != string.Empty)
            {
                if (!data.User.ContainsItem(request.MessageText))
                {
                    data.User.ExpenseCategories.Add(request.MessageText);
                    data.ComunicationChannel.SendMessage(request.User, "¡Nueva categoría de gasto agregada exitosamente! 🙌");
                    data.ClearOperation();
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Ya existe una categoría de gasto con este nombre. 🙃");
                    data.ComunicationChannel.SendMessage(request.User, "Ingrese una nueva categoría de gasto:");
                }
            }
            else
            {
                data.ComunicationChannel.SendMessage(request.User, "¡Debes ingresar una categoría de gasto!");
                data.ComunicationChannel.SendMessage(request.User, "Ingrese una nueva categoría de gasto:");
            }
        }
    }
}