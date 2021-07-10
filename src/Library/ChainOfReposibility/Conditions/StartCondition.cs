namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Condición inicial.
    /// </summary>
    public class StartConversationCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);
            return data.State == State.Start;
        }
    }
}