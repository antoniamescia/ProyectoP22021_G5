using System;

namespace Bankbot
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
        Data data = Session.Instance.GetChat(request.Id);


        if (!data.Temp.ContainsKey("username"))
        {
            if (Session.Instance.UsernameExists(request.Text))
            {
                data.Channel.SendMessage(request.Id, "Ya existe un usuario con este nombre 😟.\nVuelva a ingresar un nombre de usuario:");
            }
            else
            {
                data.Temp.Add("username", request.Text);
                data.Channel.SendMessage(request.Id, "Contraseña:");
            }
        }
        else if (!data.Temp.ContainsKey("password"))
        {
            data.Temp.Add("password", request.Text);
        }

        if (data.Temp.ContainsKey("username") && data.Temp.ContainsKey("password"))
        {
            string username = data.GetDictionaryValue<string>("username");
            string password = data.GetDictionaryValue<string>("password");

            Session.Instance.AddUser(username, password);
            User user = Session.Instance.GetUser(username, password);

            if (user != null)
            {
                data.Channel.SendMessage(request.Id, "¡Usuario creado con éxito! 🙌");
                data.Channel.SendMessage(request.Id, "¿Cómo quieres proceder?\n" + Commands.Instance.CommandList(request.Id));
            }
            // Exception 
            else
            {
                data.Channel.SendMessage(request.Id, "Lo sentimos, ha ocurrido un error. 🥲");
            }
            data.ClearOperation();
        }
    }
}
}