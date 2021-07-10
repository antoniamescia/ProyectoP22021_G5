namespace BankerBot
{
    public class MessengerHandler : AbstractHandler<IMessage>
    {
        public MessengerHandler(MessengerCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            var data = Session.Instance.GetChat(request.UserID);
            data.State = State.HandlingRequest;

            switch (request.MessageText.ToLower())
            {
                case "/comandos":
                    data.Channel.SendMessage(request.UserID, Commands.Instance.ListCommands(request.UserID));
                    data.State = State.Messenger;
                    break;

                case "/crearusuario":

                    if (data.User == null)
                    {
                        data.Command = request.MessageText.ToLower();
                        data.Channel.SendMessage(request.UserID, "Nombre de usuario:");
                        break;
                    }
                    data.Channel.SendMessage(request.UserID, "Para proceder, cierra sesión. 🙏🏼");
                    break;

                case "/iniciarsesion":
                    if (data.User == null)
                    {
                        data.Command = request.MessageText;
                        data.Channel.SendMessage(request.UserID, "Ingresa tu nombre de usuario:");
                        break;
                    }
                    data.Channel.SendMessage(request.UserID, "Para proceder, cierra sesión. 🙏🏼");
                    break;


                case "/crearcuenta":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.Channel.SendMessage(request.UserID, "¿Qué tipo de cuenta es? 💳:\n" + Account.DisplayAccountType());
                        break;
                    }

                    data.Channel.SendMessage(request.UserID, "Para proceder, inicia sesión. 🙏🏼");
                    break;

                case "/transaccion":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.Channel.SendMessage(request.UserID, "¿Qué tipo de transacción deseas realizar? :\n1 - Ingreso\n2 - Egreso");
                        break;
                    }
                    data.Channel.SendMessage(request.UserID, "Para proceder, inicia sesión. 🙏🏼");
                    break;
                
                 case "/mostrarbalance":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.Channel.SendMessage(request.UserID, "¿De qué cuenta quieres consultar el balance?\n" + data.User.DisplayAccounts());
                        break;
                    }
                    data.Channel.SendMessage(request.UserID, "Para proceder, inicia sesión. 🙏🏼");
                    break;


                case "/agregarcategoriadegasto":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.Channel.SendMessage(request.UserID, "Ingrese una nueva categoría de gasto:");
                        break;
                    }
                    data.Channel.SendMessage(request.UserID, "Para proceder, inicia sesión. 🙏🏼");
                    break;

                case "/cambiarobjetivodeahorro":
                    if (data.User != null)
                    {
                        data.Command = request.MessageText;
                        data.Channel.SendMessage(request.UserID, "¿De qué cuenta quieres cambiar el objetivo de ahorro?\n" + data.User.DisplayAccounts());
                        break;
                    }
                    data.Channel.SendMessage(request.UserID, "Para proceder, inicia sesión. 🙏🏼");
                    break;
                
                case "/convertir":

                    data.Command = request.MessageText;
                    data.Channel.SendMessage(request.UserID, "¿Desde qué moneda quieres convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                    break;


                 case "/cerrarsesion":

                    if (data.User != null)
                    {
                        data.User = null;
                        data.Channel.SendMessage(request.UserID, "¡Desconectado con éxito! 👏🏼");
                        data.Channel.SendMessage(request.UserID, "¿Cómo quieres proceder?:\n" + Commands.Instance.ListCommands((request.UserID)));
                        data.State = State.Messenger;
                        break;
                    }
                    data.Channel.SendMessage(request.UserID, "Para proceder, cierra sesión. 🙏🏼");
                    data.State = State.Messenger;
                    break;
            }
        }
    }
}