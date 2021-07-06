namespace Library
{
    public class CreateUserCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
            UserInfo info = Session.Instance.GetChatInfo(request.User);
            return info.ConversationState == ConversationState.HandlingRequest && info.Command.ToLower() == "/crearusuario";
        }
    }
}