namespace Library
{
    public class AddExpenseCategoryCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.User);
            return data.ConversationState == ConversationState.HandlingRequest && data.Command.ToLower() == "/agregarcategoriadegasto";
        }
    }
}