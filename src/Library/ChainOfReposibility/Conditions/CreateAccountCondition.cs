namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Da la posibilidad al usuario de crear una cuenta con divisa a elección.
    /// </summary>
    public class CreateAccountCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);
            return data.ConversationState == ConversationState.HandlingRequest && data.Command.ToLower() == "/crearcuenta";
        }
    }
}