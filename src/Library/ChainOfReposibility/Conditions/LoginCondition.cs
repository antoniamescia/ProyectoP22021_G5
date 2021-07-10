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
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);
            return data.ConversationState == ConversationState.HandlingRequest && data.Command.ToLower() == "/iniciarsesion";
        }
    }
}