namespace BankerBot
{
    public class MessengerHandler : AbstractHandler<IMessage>
    {
        /// <summary>
        /// Handler que segun que opcion se eliga de los comandos da su respetivo mensaje y acción
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public MessengerHandler(MessengerCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);
            data.ConversationState = ConversationState.HandlingRequest;

            switch (request.MessageText.ToLower())
            {
                case "/comandos":
                    data.ComunicationChannel.SendMessage(request.UserID, Commands.Instance.CommandList(request.UserID));
                    data.ConversationState = ConversationState.Messenger;
                    break;

                case "/crearusuario":

                    if (data.User == null)
                    {
                        data.Command = request.MessageText.ToLower();
                        data.ComunicationChannel.SendMessage(request.UserID, "Nombre de usuario:");
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.UserID, "Para proceder, cierra sesión. 🙏🏼");
                    break;

                case "/iniciarsesion":
                    if (data.User == null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.UserID, "Ingresa tu nombre de usuario:");
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.UserID, "Para proceder, cierra sesión. 🙏🏼");
                    break;

                case "/crearcuenta":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.UserID, "¿Qué tipo de cuenta es? 💳:\n" + Account.DisplayAccountType());
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.UserID, "Para proceder, inicia sesión. 🙏🏼");
                    break;

                case "/transaccion":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.UserID, "¿Qué tipo de transacción deseas realizar? :\n1 - Ingreso\n2 - Egreso");
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.UserID, "Para proceder, inicia sesión. 🙏🏼");
                    break;

                case "/mostrarbalance":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.UserID, "¿De qué cuenta quieres consultar el balance?\n" + data.User.DisplayAccounts());
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.UserID, "Para proceder, inicia sesión. 🙏🏼");
                    break;

                case "/agregarcategoriadegasto":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.UserID, "Ingrese una nueva categoría de gasto:");
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.UserID, "Para proceder, inicia sesión. 🙏🏼");
                    break;

                case "/cambiarobjetivodeahorro":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.UserID, "¿De qué cuenta quieres cambiar el objetivo de ahorro?\n" + data.User.DisplayAccounts());
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.UserID, "Para proceder, inicia sesión. 🙏🏼");
                    break;

                case "/convertir":

                    data.Command = request.MessageText;
                    data.ComunicationChannel.SendMessage(request.UserID, "¿Desde qué moneda quieres convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                    break;


                case "/cerrarsesion":

                    if (data.User != null)
                    {
                        data.User = null;
                        data.ComunicationChannel.SendMessage(request.UserID, "¡Desconectado con éxito! 👏🏼");
                        data.ComunicationChannel.SendMessage(request.UserID, "¿Cómo quieres proceder?:\n" + Commands.Instance.CommandList((request.UserID)));
                        data.ConversationState = ConversationState.Messenger;
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.UserID, "Para proceder, cierra sesión. 🙏🏼");
                    data.ConversationState = ConversationState.Messenger;
                    break;

                case "/verhistorialdetransacciones":

                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.ComunicationChannel.SendMessage(request.UserID, "Seleccione una cuenta para ver el historial:\n" + data.User.DisplayAccounts());
                        break;
                    }
                    data.ComunicationChannel.SendMessage(request.UserID, "Para proceder, inicie sesión. 🙏🏼");
                    break;

            }
        }
    }
}