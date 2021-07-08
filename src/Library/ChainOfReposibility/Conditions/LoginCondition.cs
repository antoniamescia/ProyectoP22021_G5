namespace Bankbot
{
    /*Cumple con EXPERT y SRP*/

    /// <summary>
    /// Condición para loguearse.
    /// </summary>
    public class LoginCondition : ICondition<IMessage>
    {
        public bool IsSatisfied(IMessage request)
        {
            var data = Session.Instance.GetChat(request.Id);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/iniciarsesion";
        }
    }
}