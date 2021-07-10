namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Se encarga de borrar un usuario si así se desea.
    /// </summary>
    public class DeleteUserCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);
            return data.ConversationState == ConversationState.HandlingRequest && data.Command.ToLower() == "/borrarusuario";
        }
    }
}