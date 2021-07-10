namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    public class MessengerCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);
            return data.ConversationState == ConversationState.Messenger && Commands.Instance.CommandExists(request.MessageText);
        }
    }
}