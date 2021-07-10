using System;

namespace Bankbot
{
    public class LoginHandler : AbstractHandler<IMessage>
    {
        /*Cumple con ## SRP ## 
        Cumple con ## EXPERT ##*/

        /// <summary>
        /// Handler para loguearte con un usuario existente.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public LoginHandler(LoginCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);

            if (!data.Temp.ContainsKey("username"))
            {
                data.Temp.Add("username", request.Text);
                data.Channel.SendMessage(request.Id, "Ingresa tu contraseña:");
            }
            else if (!data.Temp.ContainsKey("password"))
            {
                data.Temp.Add("password", request.Text);
            }

            if (data.Temp.ContainsKey("username") && data.Temp.ContainsKey("password"))
            {
                string username = data.GetDictionaryValue<string>("username");
                string password = data.GetDictionaryValue<string>("password");
                var user = Session.Instance.GetUser(username, password);

                bool connected = false;

                foreach (var item in Session.Instance.DataMap)
                {
                    if (item.Value.User != null && item.Value.User == user) connected = true;
                }

                if (!connected && user != null)
                {
                    data.User = user;
                    data.Channel.SendMessage(request.Id, "¡Inicio de sesión exitoso! 💪🏼");
                    data.Channel.SendMessage(request.Id, "¿Cómo quieres proceder?:\n" + Commands.Instance.CommandList((request.Id)));
                }
                else if (connected)
                {
                    data.Channel.SendMessage(request.Id, "¡Ups! Ya estás conectado. 😆");
                }
                else
                {
                    data.Channel.SendMessage(request.Id, "Inicio de sesión falló. 😞 Vuelve a intentarlo.");
                }

                data.ClearOperation();
            }
        }
    }
}