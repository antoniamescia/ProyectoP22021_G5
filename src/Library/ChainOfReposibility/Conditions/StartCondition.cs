namespace Bankbot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Condición inicial.
    /// </summary>
    public class StartConversationCondition : ICondition<IMessage>
    {
        public bool IsSatisfied(IMessage request)
        {
            var data = Session.Instance.GetChat(request.Id);
            return data.State == State.Start;
        }
    }
}