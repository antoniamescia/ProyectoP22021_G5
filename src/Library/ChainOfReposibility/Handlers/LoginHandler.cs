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
            UserInfo data = Session.Instance.GetChatInfo(request.User);

            if (!data.ProvisionalInfo.ContainsKey("username"))
            {
                data.ProvisionalInfo.Add("username", request.MessageText);
                data.ComunicationChannel.SendMessage(request.User, "Ingrese una contraseña:");
            }
            else if (!data.ProvisionalInfo.ContainsKey("password"))
            {
                data.ProvisionalInfo.Add("password", request.MessageText);
            }

            if (data.ProvisionalInfo.ContainsKey("username") && data.ProvisionalInfo.ContainsKey("password"))
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