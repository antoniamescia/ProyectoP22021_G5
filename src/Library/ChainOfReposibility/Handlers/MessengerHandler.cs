namespace Library
{
    public class MessengerHandler : AbstractHandler<UserMessage>
    {
        public MessengerHandler(MessengerCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);
            data.ConversationState = ConversationState.HandlingRequest;

            switch (request.MessageText.ToLower())
            {
                case "/Comandos":
                    data.ComunicationChannel.SendMessage(request.User, Commands.Instance.CommandList(request.User));
                    data.ConversationState = ConversationState.Messenger;
                    break;

                case "/CrearUsuario":

                    if (data.User == null)
                    {
                        data.Command = request.MessageText.ToLower();
                        data.ComunicationChannel.SendMessage(request.User, "Ingresa tu nombre de usuario:");
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.User, "Para realizar esta acción, por favor cierra sesión. 🙏🏼");
                    break;

                case "/IniciarSesion":
                    if (data.User == null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.User, "Ingresa tu nombre de usuario:");
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.User, "Para realizar esta acción, por favor cierra sesión. 🙏🏼");
                    break;

                case "/CerrarSesion":

                    if (data.User != null)
                    {
                        data.User = null;
                        data.ComunicationChannel.SendMessage(request.User, "¡Desconectado con éxito!");
                        data.ComunicationChannel.SendMessage(request.User, "¿Cómo deseas continuar?:\n" + Commands.Instance.CommandList((request.User)));
                        data.ConversationState = ConversationState.Messenger;
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    data.ConversationState = ConversationState.Messenger;
                    break;

                case "/CrearCuenta":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        // data.ComunicationChannel.SendMessage(request.User, "Ingrese el tipo de cuenta:\n" + Account.ShowAccountType());
                        break;
                    }

                    data.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/Convertir":

                    data.Command = request.MessageText;
                    data.ComunicationChannel.SendMessage(request.User, "¿Qué moneda deseas convertir? 🪙:\n" + CurrencyExchanger.Instance.ShowCurrencyList());
                    break;

                case "/BorrarUsuario":

                    data.Command = request.MessageText;
                    data.ComunicationChannel.SendMessage(request.User, "Ingresa el nombre de usuario que deseas eliminar:");
                    break;

                case "/BorrarCuenta":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.User, "¿Qué cuenta deseas eliminar?:\n" + data.User.DisplayAccounts());
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/Transaccion":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.User, "¿Qué tipo de transacción quieres realizar?:\n1 - Ingreso🤑\n2 - Egreso👋🏼");
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/AgregarCategoriaDeGasto":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.User, "Ingresa una nueva categoría de gasto: 💸");
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/CambiarObjetivoDeAhorro":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.User, "¿De qué cuenta quieres cambiar el objetivo de ahorro?\n" + data.User.DisplayAccounts());
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/AgregarMoneda":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.User, "Ingresa el tipo de la nueva moneda:");
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;

                case "/balanceDeCuenta":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.User, "¿De qué cuenta deseas consultar el balance?\n" + data.User.DisplayAccounts());
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.User, "Para realizar está acción, por favor inicia sesión. 🙏🏼");
                    break;
            }
        }

    }
}