using System;

namespace BankerBot
{
    public class LoginHandler : AbstractHandler<IMessage>
    {
        /// <summary>
        /// Handler que se encargará de logear al usuario.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        
        /*
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método HandleRequest.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa el método polimórfico handleRequest.
        Cumple con el patrón Chain of Responsibility.
        */
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
                EndUser user = Session.Instance.GetUser(username, password);
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