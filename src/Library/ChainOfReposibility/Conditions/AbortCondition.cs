namespace Library
{
    public class AbortCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);
            return info.ConversationState != ConversationState.Messenger && request.MessageText.ToLower() == "/salir";
        }
    }
}