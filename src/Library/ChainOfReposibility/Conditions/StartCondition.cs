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
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);
            return data.ConversationState == ConversationState.Start;
        }
    }
}