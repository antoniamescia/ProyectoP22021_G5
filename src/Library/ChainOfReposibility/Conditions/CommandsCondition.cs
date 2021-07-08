namespace Bankbot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Clase que se encarga de recibir un comando y mostrar el resto al apuntar a otro handler.
    /// </summary>
    public class CommandsCondition : ICondition<IMessage>
    {
        public bool IsSatisfied(IMessage request)
        {
            var data = Session.Instance.GetChat(request.Id);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/comandos";
        }
    }
}