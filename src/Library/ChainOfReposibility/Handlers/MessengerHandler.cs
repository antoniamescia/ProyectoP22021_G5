namespace Library
{
    public class MessengerHandler : AbstractHandler<UserMessage>
    {
        public MessengerHandler(MessengerCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);
            info.ConversationState = ConversationState.HandlingRequest;

            switch (request.MessageText.ToLower())
            {
                case "/Comandos":
                    info.ComunicationChannel.SendMessage(request.User, Commands.Instance.CommandList(request.User));
                    info.ConversationState = ConversationState.Messenger;
                    break;

                case "/CrearUsuario":

                    if (info.User == null)
                    {
                        info.Command = request.MessageText.ToLower();
                        info.ComunicationChannel.SendMessage(request.User, "Ingresa tu nombre de usuario:");
                        break;
                    }
                    info.ComunicationChannel.SendMessage(request.User, "Para realizar esta acción, por favor cierra sesión. 🙏🏼");
                    break;

                case "/IniciarSesion":
                    if (info.User == null)
                    {
                        info.Command = request.MessageText;
                        info.ComunicationChannel.SendMessage(request.User, "Ingresa tu nombre de usuario:");
                        break;
                    }
                    info.ComunicationChannel.SendMessage(request.User, "Para realizar esta acción, por favor cierra sesión. 🙏🏼");
                    break;

                case "/CerrarSesion":

                    if (info.User != null)
                    {
                        info.User = null;
                        info.ComunicationChannel.SendMessage(request.User, "¡Desconectado con éxito!");
                        info.ComunicationChannel.SendMessage(request.User, "¿Cómo deseas continuar?:\n" + Commands.Instance.CommandList((request.User)));
                        info.ConversationState = ConversationState.Messenger;
                        break;
                    }
                    info.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    info.ConversationState = ConversationState.Messenger;
                    break;

                case "/CrearCuenta":
                    if (info.User != null)
                    {
                        info.Command = request.MessageText;
                        // info.ComunicationChannel.SendMessage(request.User, "Ingrese el tipo de cuenta:\n" + Account.ShowAccountType());
                        break;
                    }

                    info.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/Convertir":

                    info.Command = request.MessageText;
                    info.ComunicationChannel.SendMessage(request.User, "¿Qué moneda deseas convertir? 🪙:\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                    break;

                case "/BorrarUsuario":

                    info.Command = request.MessageText;
                    info.ComunicationChannel.SendMessage(request.User, "Ingresa el nombre de usuario que deseas eliminar:");
                    break;

                case "/BorrarCuenta":
                    if (info.User != null)
                    {
                        info.Command = request.MessageText;
                        info.ComunicationChannel.SendMessage(request.User, "¿Qué cuenta deseas eliminar?:\n" + info.User.DisplayAccounts());
                        break;
                    }
                    info.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/Transaccion":
                    if (info.User != null)
                    {
                        info.Command = request.MessageText;
                        info.ComunicationChannel.SendMessage(request.User, "¿Qué tipo de transacción quieres realizar?:\n1 - Ingreso🤑\n2 - Egreso👋🏼");
                        break;
                    }
                    info.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/AgregarCategoriaDeGasto":
                    if (info.User != null)
                    {
                        info.Command = request.MessageText;
                        info.ComunicationChannel.SendMessage(request.User, "Ingresa una nueva categoría de gasto: 💸");
                        break;
                    }
                    info.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/CambiarObjetivoDeAhorro":
                    if (info.User != null)
                    {
                        info.Command = request.MessageText;
                        info.ComunicationChannel.SendMessage(request.User, "¿De qué cuenta quieres cambiar el objetivo de ahorro?\n" + info.User.DisplayAccounts());
                        break;
                    }
                    info.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/AgregarMoneda":
                    if (info.User != null)
                    {
                        info.Command = request.MessageText;
                        info.ComunicationChannel.SendMessage(request.User, "Ingresa el tipo de la nueva moneda:");
                        break;
                    }
                    info.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/balanceDeCuenta":
                    if (info.User != null)
                    {
                        info.Command = request.MessageText;
                        info.ComunicationChannel.SendMessage(request.User, "¿De qué cuenta deseas consultar el balance?\n" + info.User.DisplayAccounts());
                        break;
                    }
                    info.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;
            }
        }

    }
}