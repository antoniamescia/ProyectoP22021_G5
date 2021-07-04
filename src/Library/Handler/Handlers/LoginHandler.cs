using System;

namespace Library
{
    public class LoginHandler : AbstractHandler<UserMessage>
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

        protected override void handleRequest(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);

            if (!data.Temp.ContainsKey("username"))
            {
                data.Temp.Add("username", request.MessageText);
                data.ComunicationChannel.SendMessage(request.User, "Ingrese una contraseña:");
            }
            else if (!data.Temp.ContainsKey("password"))
            {
                data.Temp.Add("password", request.MessageText);
            }

            if (data.Temp.ContainsKey("username") && data.Temp.ContainsKey("password"))
            {
                string username = data.GetDictionaryValue<string>("username");
                string password = data.GetDictionaryValue<string>("password");
                var user = Session.Instance.GetEndUser(username, password);

                bool connected = false;

                foreach (var item in Session.Instance.UserInfoMap)
                {
                    if (item.Value.User != null && item.Value.User == user) connected = true;
                }

                if (!connected && user != null)
                {
                    data.User = user;
                    data.ComunicationChannel.SendMessage(request.User, "Se ha conectado correctamente.");
                    data.ComunicationChannel.SendMessage(request.User, "Para continuar puedes ingresar los siguientes comandos:\n" + Commands.Instance.CommandList((request.User)));
                }
                else if (connected)
                {
                    data.ComunicationChannel.SendMessage(request.User, "Este usuario ya se encuentra conectado.");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.User, "Credenciales incorrectas, vuelva a intentarlo.");
                }

                data.ClearOperation();
            }
        }
    }
}