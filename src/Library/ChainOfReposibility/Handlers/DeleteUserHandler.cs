using System;
using System.Collections.Generic;

namespace Bankbot
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/

    /// <summary>
    /// Handler para borrar el usuario.
    /// </summary>
    public class DeleteUserHandler : AbstractHandler<IMessage>
    {
        public DeleteUserHandler(DeleteUserCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            var data = Session.Instance.GetChat(request.Id);

            if (!data.Temp.ContainsKey("username"))
            {
                data.Temp.Add("username", request.Text);
                data.Channel.SendMessage(request.Id, "Ingrese una constraseña:");

            }
            else if (!data.Temp.ContainsKey("password"))
            {
                data.Temp.Add("password", request.Text);
                data.Channel.SendMessage(request.Id, "¿Estas seguro que deseas borrar este usuario? Vuelva a ingresar su contraseña:");
            }

            else if (data.Temp.ContainsKey("username") && data.Temp.ContainsKey("password"))
            {
                string username = data.GetDictionaryValue<string>("username");
                string password = data.GetDictionaryValue<string>("password");

                User user = Session.Instance.GetUser(username, password);

                if (user == null)
                {
                    data.Channel.SendMessage(request.Id, "Credenciales incorrectas, vuelva a intentarlo.");
                    data.ClearOperation();
                }
                else
                {
                    data.Temp.Add("confirmation", "");
                }

            }

            if (data.Temp.ContainsKey("confirmation"))
            {
                string username = data.GetDictionaryValue<string>("username");
                string password = data.GetDictionaryValue<string>("password");

                string confirmationPassword = request.Text;

                if (password == confirmationPassword)
                {
                    Session.Instance.RemoveUser(username, password);
                    foreach (var item in Session.Instance.DataMap)
                    {
                        if (item.Value.User != null && item.Value.User.Username == username)
                        {
                            item.Value.User = null;
                            item.Value.ClearOperation();
                        }
                    }
                    data.Channel.SendMessage(request.Id, "Usuario eliminado correctamente.");
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "Credenciales incorrectas.");
                }

                data.ClearOperation();
            }
        }
    }
}
