namespace BankerBot
{
    public class ShowCommandsCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.User);
            return data.ConversationState == ConversationState.HandlingRequest && data.Command.ToLower() == "/comandos";
        }
    }
}