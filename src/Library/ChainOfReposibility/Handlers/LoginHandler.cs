using System;

namespace BankerBot
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
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);

            if (!data.ProvisionalInfo.ContainsKey("username"))
            {
                data.ProvisionalInfo.Add("username", request.MessageText);
                data.ComunicationChannel.SendMessage(request.UserID, "Ingresa tu contraseña:");
            }
            else if (!data.ProvisionalInfo.ContainsKey("password"))
            {
                data.ProvisionalInfo.Add("password", request.MessageText);
            }

            if (data.ProvisionalInfo.ContainsKey("username") && data.ProvisionalInfo.ContainsKey("password"))
            {
                string username = data.GetDictionaryValue<string>("username");
                string password = data.GetDictionaryValue<string>("password");
                var user = Session.Instance.GetUser(username, password);

                bool connected = false;

                foreach (var item in Session.Instance.UserInfoMap)
                {
                    if (item.Value.User != null && item.Value.User == user) connected = true;
                }

                if (!connected && user != null)
                {
                    data.User = user;
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Inicio de sesión exitoso! 💪🏼");
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Cómo quieres proceder?:\n" + Commands.Instance.CommandList((request.UserID)));
                }
                else if (connected)
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "¡Ups! Ya estás conectado. 😆");
                }
                else
                {
                    data.ComunicationChannel.SendMessage(request.UserID, "Inicio de sesión falló. 😞 Vuelve a intentarlo.");
                }

                data.ClearOperation();
            }
        }
    }
}