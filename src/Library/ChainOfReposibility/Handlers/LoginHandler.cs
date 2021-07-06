using System;

namespace Library
{
    // ESTE HANDLER VER QUÉ ONDA, A VER SI REALMENTE LO NECESITAMOS! 
    public class LoginHandler : AbstractHandler<UserMessage>
    {
        public LoginHandler(LoginCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);

            if (!info.ProvisionalInfo.ContainsKey("username"))
            {
                info.ProvisionalInfo.Add("username", request.MessageText);
                info.ComunicationChannel.SendMessage(request.User, "Ingrese una contraseña:");
            }
            else if (!info.ProvisionalInfo.ContainsKey("password"))
            {
                info.ProvisionalInfo.Add("password", request.MessageText);
            }

            if (info.ProvisionalInfo.ContainsKey("username") && info.ProvisionalInfo.ContainsKey("password"))
            {
                string username = info.GetDictionaryValue<string>("username");
                string password = info.GetDictionaryValue<string>("password");
                var user = Session.Instance.GetEndUser(username, password);

                bool connected = false;

                foreach (var item in Session.Instance.UserInfoMap)
                {
                    if (item.Value.User != null && item.Value.User == user) connected = true;
                }

                if (!connected && user != null)
                {
                    info.User = user;
                    info.ComunicationChannel.SendMessage(request.User, "Se ha conectado correctamente.");
                    info.ComunicationChannel.SendMessage(request.User, "Para continuar puedes ingresar los siguientes comandos:\n" + Commands.Instance.CommandList((request.User)));
                }
                else if (connected)
                {
                    info.ComunicationChannel.SendMessage(request.User, "Este usuario ya se encuentra conectado.");
                }
                else
                {
                    info.ComunicationChannel.SendMessage(request.User, "Credenciales incorrectas, vuelva a intentarlo.");
                }

                info.ClearOperation();
            }
        }
    }
}