using System;

namespace Library
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/

    /// <summary>
    /// Handler para crearel usuario.
    /// </summary>
    public class CreateUserHandler : AbstractHandler<UserMessage>
    {
        public CreateUserHandler(CreateUserCondition condition) : base(condition)
        {
        }

    protected override void handleRequest(UserMessage request)
    {
        var data = Session.Instance.GetChatInfo(request.User);


        if (!data.Temp.ContainsKey("username"))
        {
            if (Session.Instance.UsernameExists(request.MessageText))
            {
                data.ComunicationChannel.SendMessage(request.User, "Ya existe un usuario con este nombre 😟.\nVuelva a ingresar un nombre de usuario:");
            }
            else
            {
                data.Temp.Add("username", request.MessageText);
                data.ComunicationChannel.SendMessage(request.User, "Ingrese una contraseña:");
            }
        }
        else if (!data.Temp.ContainsKey("password"))
        {
            data.Temp.Add("password", request.MessageText);
        }

        if (data.Temp.ContainsKey("username") && data.Temp.ContainsKey("password"))
        {
            string username = data.GetDictionaryValue<string>("username");
            string password = data.GetDictionaryValue<string>("password");

            Session.Instance.AddUser(username, password);
            EndUser user = Session.Instance.GetEndUser(username, password);

            if (user != null)
            {
                data.ComunicationChannel.SendMessage(request.User, "Usuario creado correctamente.");
                data.ComunicationChannel.SendMessage(request.User, "Elija un comando de la siguiente lista:\n" + Commands.Instance.CommandList(request.User));
            }
            // Exception 
            else
            {
                data.ComunicationChannel.SendMessage(request.User, "Ha ocurrido un error.");
            }
            data.ClearOperation();
        }
    }
}
}