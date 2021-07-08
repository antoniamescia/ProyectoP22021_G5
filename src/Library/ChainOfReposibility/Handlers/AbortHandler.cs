using System;


namespace Bankbot
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/
    /// <summary>
    /// Handler para cancelar una opción o salir.
    /// </summary>
    public class AbortHandler : AbstractHandler<IMessage>
    {
        public AbortHandler(AbortCondition condition) : base(condition)
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