namespace Bankbot
{
    public class MessengerHandler : AbstractHandler<IMessage>
    {
        public MessengerHandler(MessengerCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            var data = Session.Instance.GetChat(request.Id);
            data.State = State.HandlingRequest;

            switch (request.Text.ToLower())
            {
                case "/comandos":
                    data.Channel.SendMessage(request.Id, Commands.Instance.CommandList(request.Id));
                    data.State = State.Messenger;
                    break;

                case "/crearusuario":

                    if (data.User == null)
                    {
                        data.Command = request.Text.ToLower();
                        data.Channel.SendMessage(request.Id, "Ingresa el nombre de usuario:");
                        break;
                    }
                    data.Channel.SendMessage(request.Id, "Para proceder, por favor cierra sesión.");
                    break;

                case "/iniciarsesion":
                    if (data.User == null)
                    {
                        data.Command = request.Text;
                        data.Channel.SendMessage(request.Id, "Ingrese el nombre de usuario:");
                        break;
                    }
                    data.Channel.SendMessage(request.Id, "Para proceder, por favor cierra sesión.");
                    break;

                case "/cerrarsesion":

                    if (data.User != null)
                    {
                        data.User = null;
                        data.Channel.SendMessage(request.Id, "¡Desconectado con éxito!");
                        data.Channel.SendMessage(request.Id, "¿Cómo quieres proceder?:\n" + Commands.Instance.CommandList((request.Id)));
                        data.State = State.Messenger;
                        break;
                    }
                    data.Channel.SendMessage(request.Id, "Para proceder, por favor cierra sesión.");
                    data.State = State.Messenger;
                    break;

                case "/crearcuenta":
                    if (data.User != null)
                    {
                        data.Command = request.Text;
                        data.Channel.SendMessage(request.Id, "¿Qué tipo de cuenta es?:\n" + Account.ShowAccountType());
                        break;
                    }

                    data.Channel.SendMessage(request.Id, "Para proceder, por favor inicia sesión.");
                    break;

                case "/convertir":

                    data.Command = request.Text;
                    data.Channel.SendMessage(request.Id, "¿Desde qué moneda quieres convertir? 🪙\n" + CurrencyExchanger.Instance.DisplayCurrencyList());
                    break;

                case "/borrarusuario":

                    data.Command = request.Text;
                    data.Channel.SendMessage(request.Id, "¿Qué usuario deseas eliminar?");
                    break;


                case "/transaccion":
                    if (data.User != null)
                    {
                        data.Command = request.Text;
                        data.Channel.SendMessage(request.Id, "¿Qué tipo de transacción deseas realizar? :\n1 - Ingreso\n2 - Egreso");
                        break;
                    }
                    data.Channel.SendMessage(request.Id, "Para proceder, por favor inicia sesión.");
                    break;


                case "/agregarcategoriadegasto":
                    if (data.User != null)
                    {
                        data.Command = request.Text;
                        data.Channel.SendMessage(request.Id, "Ingrese una nueva categoría de gasto:");
                        break;
                    }
                    data.Channel.SendMessage(request.Id, "Para proceder, por favor inicia sesión.");
                    break;

                case "/cambiarobjetivo":
                    if (data.User != null)
                    {
                        data.Command = request.Text;
                        data.Channel.SendMessage(request.Id, "¿De qué cuenta quieres cambiar el objetivo de ahorro?\n" + data.User.ShowAccountList());
                        break;
                    }
                    data.Channel.SendMessage(request.Id, "Para proceder, por favor inicia sesión.");
                    break;

                case "/agregarmoneda":
                    if (data.User != null)
                    {
                        data.Command = request.Text;
                        data.Channel.SendMessage(request.Id, "Ingrese el símbolo de la nueva moneda: 💲");
                        break;
                    }
                    data.Channel.SendMessage(request.Id, "Para proceder, por favor inicia sesión.");
                    break;

                case "/mostrarbalance":
                    if (data.User != null)
                    {
                        data.Command = request.Text;
                        data.Channel.SendMessage(request.Id, "¿De qué cuenta quieres consultar el balance?\n" + data.User.ShowAccountList());
                        break;
                    }
                    data.Channel.SendMessage(request.Id, "Para proceder, por favor inicia sesión.");
                    break;
            }
        }
    }
}