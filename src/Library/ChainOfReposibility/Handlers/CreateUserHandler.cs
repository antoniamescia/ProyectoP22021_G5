using System;

namespace BankerBot
{
    
    /// <summary>
    /// Handler que se encargará de crear un nuevo usuario.
    /// </summary>
    public class CreateUserHandler : AbstractHandler<IMessage>
    {
        /*
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método HandleRequest.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa el método polimórfico handleRequest.
        Cumple con el patrón Chain of Responsibility.
        */
        public CreateUserHandler(CreateUserCondition condition) : base(condition)
        {
        }

    protected override void handleRequest(IMessage request)
    {
        UserInfo data = Session.Instance.GetChatInfo(request.UserID);


        if (!data.ProvisionalInfo.ContainsKey("username"))
        {
            if (Session.Instance.UsernameExists(request.MessageText))
            {
                data.ComunicationChannel.SendMessage(request.UserID, "Ya existe un usuario con este nombre 😟.\nVuelva a ingresar un nombre de usuario:");
            }
            else
            {
                data.ProvisionalInfo.Add("username", request.MessageText);
                data.ComunicationChannel.SendMessage(request.UserID, "Contraseña:");
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
            EndUser user = Session.Instance.GetUser(username, password);

            if (user != null)
            {
                data.ComunicationChannel.SendMessage(request.UserID, "¡Usuario creado con éxito! 🙌");
                data.ComunicationChannel.SendMessage(request.UserID, "¿Cómo quieres proceder?\n" + Commands.Instance.CommandList(request.UserID));
            }
            else
            {
                data.ComunicationChannel.SendMessage(request.UserID, "Lo sentimos, ha ocurrido un error. 🥲");
            }
            data.ClearOperation();
        }
    }
}
}