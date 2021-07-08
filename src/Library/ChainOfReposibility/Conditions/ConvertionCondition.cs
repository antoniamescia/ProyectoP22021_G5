namespace Bankbot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Se encarga de dar la posibilidad de convertir al usuario.
    /// </summary>
    public class ConvertionCondition : ICondition<IMessage>
    {
        public bool IsSatisfied(IMessage request)
        {
            var data = Session.Instance.GetChat(request.Id);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/convertir";
        }
    }
}