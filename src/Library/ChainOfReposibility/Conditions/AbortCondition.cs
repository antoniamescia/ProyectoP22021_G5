namespace Library
{
    public class AbortCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.User);
            return data.ConversationState != ConversationState.Messenger && request.MessageText.ToLower() == "/salir";
        }
    }
}