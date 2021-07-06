namespace Library
{
    public class DeleteAccountCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);
            return info.ConversationState == ConversationState.HandlingRequest && info.Command.ToLower() == "/borrarcuenta";
        }
    }
}