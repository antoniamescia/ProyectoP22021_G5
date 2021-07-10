namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Se encarga de dar la posibilidad de convertir al usuario.
    /// </summary>
    public class ConvertionCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);
            return data.ConversationState == ConversationState.HandlingRequest && data.Command.ToLower() == "/convertir";
        }
    }
}