using System;

namespace Library
{
    public class CreateUserHandler : AbstractHandler<UserMessage>
    {
        public CreateUserHandler(CreateUserCondition condition) : base(condition)
        {
        }

    protected override void handleRequest(UserMessage request)
    {
        UserInfo data = Session.Instance.GetChatInfo(request.User);


        if (!data.ProvisionalInfo.ContainsKey("username"))
        {
            if (Session.Instance.UsernameExists(request.MessageText))
            {
                data.ComunicationChannel.SendMessage(request.User, "Ya existe un usuario con este nombre 😟.\n Ingrese un nombre de usuario diferente:");
            }
            else
            {
                data.ProvisionalInfo.Add("username", request.MessageText);
                data.ComunicationChannel.SendMessage(request.User, "Ingrese una contraseña:");
            }
        }
        else if (!data.ProvisionalInfo.ContainsKey("password"))
        {
            data.ProvisionalInfo.Add("password", request.MessageText);
        }

        if (data.ProvisionalInfo.ContainsKey("username") && data.ProvisionalInfo.ContainsKey("password"))
        {
            string username = data.GetDictionaryValue<string>("username");
            string password = data.GetDictionaryValue<string>("password");

            Session.Instance.AddUser(username, password);
            EndUser user = Session.Instance.GetEndUser(username, password);

            if (user != null)
            {
                data.ComunicationChannel.SendMessage(request.User, "¡Usuario creado exitosamente! 🥳");
                data.ComunicationChannel.SendMessage(request.User, "¿Cómo quiere proceder?:\n" + Commands.Instance.CommandList(request.User));
            }
           
            else
            {
                data.ComunicationChannel.SendMessage(request.User, "Ha ocurrido un error. 😔");
            }
            data.ClearOperation();
        }
    }
}
}