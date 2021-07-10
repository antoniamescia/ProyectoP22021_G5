using System;

namespace BankerBot
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/

    /// <summary>
    /// Handler para crearel usuario.
    /// </summary>
    public class CreateUserHandler : AbstractHandler<IMessage>
    {
        public CreateUserHandler(CreateUserCondition condition) : base(condition)
        {
        }

    protected override void handleRequest(IMessage request)
    {
        Data data = Session.Instance.GetChat(request.UserID);


        if (!data.ProvisionalInfo.ContainsKey("username"))
        {
            if (Session.Instance.UsernameExists(request.MessageText))
            {
                data.Channel.SendMessage(request.UserID, "Ya existe un usuario con este nombre 😟.\nVuelva a ingresar un nombre de usuario:");
            }
            else
            {
                data.ProvisionalInfo.Add("username", request.MessageText);
                data.Channel.SendMessage(request.UserID, "Contraseña:");
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
            User user = Session.Instance.GetUser(username, password);

            if (user != null)
            {
                data.Channel.SendMessage(request.UserID, "¡Usuario creado con éxito! 🙌");
                data.Channel.SendMessage(request.UserID, "¿Cómo quieres proceder?\n" + Commands.Instance.ListCommands(request.UserID));
            }
            // Exception 
            else
            {
                data.Channel.SendMessage(request.UserID, "Lo sentimos, ha ocurrido un error. 🥲");
            }
            data.ClearOperation();
        }
    }
}
}