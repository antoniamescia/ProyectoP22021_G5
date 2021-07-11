namespace BankerBot
{
    
    /// <summary>
    /// Clase que se encarga de recibir un comando y mostrar el resto al apuntar a otro handler.
    /// </summary>
    public class CommandsCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);
            return data.ConversationState == ConversationState.HandlingRequest && data.Command.ToLower() == "/comandos";
        }
    }
}