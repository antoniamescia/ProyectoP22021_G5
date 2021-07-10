using System;


namespace BankerBot
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/
    /// <summary>
    /// Handler para cancelar una opción o salir.
    /// </summary>
    public class ExitHandler : AbstractHandler<IMessage>
    {
        public ExitHandler(ExitCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);

            if (data.Command != string.Empty)
            {
                data.Channel.SendMessage(request.Id, "¡Operación cancelada! ❌");
            }
        }
    }
}