namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Clase que se encarga de recibir un comando y mostrar el resto al apuntar a otro handler.
    /// </summary>
    public class CommandsCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/comandos";
        }
    }
}