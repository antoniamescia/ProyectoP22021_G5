namespace Library
{
   
    public class InitCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.User);
            return data.ConversationState == ConversationState.Start;
        }
    }
}