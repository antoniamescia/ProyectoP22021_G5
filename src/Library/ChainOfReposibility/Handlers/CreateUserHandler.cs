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
        UserInfo info = Session.Instance.GetChatInfo(request.User);


        if (!info.ProvisionalInfo.ContainsKey("username"))
        {
            if (Session.Instance.UsernameExists(request.MessageText))
            {
                info.ComunicationChannel.SendMessage(request.User, "Ya existe un usuario con este nombre 😟.\n Ingrese un nombre de usuario diferente:");
            }
            else
            {
                info.ProvisionalInfo.Add("username", request.MessageText);
                info.ComunicationChannel.SendMessage(request.User, "Ingrese una contraseña:");
            }
        }
        else if (!info.ProvisionalInfo.ContainsKey("password"))
        {
            info.ProvisionalInfo.Add("password", request.MessageText);
        }

        if (info.ProvisionalInfo.ContainsKey("username") && info.ProvisionalInfo.ContainsKey("password"))
        {
            string username = info.GetDictionaryValue<string>("username");
            string password = info.GetDictionaryValue<string>("password");

            Session.Instance.AddUser(username, password);
            EndUser user = Session.Instance.GetEndUser(username, password);

            if (user != null)
            {
                info.ComunicationChannel.SendMessage(request.User, "¡Usuario creado exitosamente! 🥳");
                info.ComunicationChannel.SendMessage(request.User, "¿Cómo quiere proceder?:\n" + Commands.Instance.CommandList(request.User));
            }
           
            else
            {
                info.ComunicationChannel.SendMessage(request.User, "Ha ocurrido un error. 😔");
            }
            info.ClearOperation();
        }
    }
}
}