using System;

namespace Library
{
    public class AddExpenseCategoryHandler : AbstractHandler<UserMessage>
    {
        public AddExpenseCategoryHandler(AddExpenseCategoryCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);

            if (request.MessageText != string.Empty)
            {
                if (!info.User.ContainsItem(request.MessageText))
                {
                    info.User.ExpenseCategories.Add(request.MessageText);
                    info.ComunicationChannel.SendMessage(request.User, "¡Nueva categoría de gasto agregada exitosamente! 🙌");
                    info.ClearOperation();
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Ya existe una categoría de gasto con este nombre. 🙃");
                    info.ComunicationChannel.SendMessage(request.User, "Ingrese una nueva categoría de gasto:");
                }
            }
            else
            {
                info.ComunicationChannel.SendMessage(request.User, "¡Debes ingresar una categoría de gasto!");
                info.ComunicationChannel.SendMessage(request.User, "Ingrese una nueva categoría de gasto:");
            }
        }
    }
}