namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/

    /// <summary>
    /// Condición para loguearse.
    /// </summary>
    public class LoginCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/iniciarsesion";
        }
    }
}