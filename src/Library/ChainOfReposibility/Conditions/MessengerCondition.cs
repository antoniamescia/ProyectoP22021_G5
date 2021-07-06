namespace BankerBot
{
    public class MessengerCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.User);
            return data.ConversationState == ConversationState.Messenger && Commands.Instance.CommandExists(request.MessageText);
        }

    }
}