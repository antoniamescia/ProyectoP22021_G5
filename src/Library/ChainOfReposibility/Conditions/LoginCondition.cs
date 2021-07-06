namespace Library
{
    public class LoginCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);
            return info.ConversationState == ConversationState.HandlingRequest && info.Command.ToLower() == "/iniciarsesion";
        }
    }
}