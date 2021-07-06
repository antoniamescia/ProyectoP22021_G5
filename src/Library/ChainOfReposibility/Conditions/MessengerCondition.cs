namespace Library
{
    public class MessengerCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);
            return info.ConversationState == ConversationState.Messenger && Commands.Instance.CommandExists(request.MessageText);
        }

    }
}