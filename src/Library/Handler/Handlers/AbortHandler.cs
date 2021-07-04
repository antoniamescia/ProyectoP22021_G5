using System;


namespace Library
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/
    /// <summary>
    /// Handler para cancelar una opción o salir.
    /// </summary>
    public class AbortHandler : AbstractHandler<UserMessage>
    {
        public AbortHandler(AbortCondition condition) : base(condition)
        {
        }

        protected override void handleRequest(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);

            if (data.Command != string.Empty)
            {
                data.ComunicationChannel.SendMessage(request.User, "Operación cancelada.");
                data.ClearOperation();
            }
            else
            {
                data.ComunicationChannel.SendMessage(request.User, "No puedo cancelar una operación que no existe.");
            }
        }
    }
}