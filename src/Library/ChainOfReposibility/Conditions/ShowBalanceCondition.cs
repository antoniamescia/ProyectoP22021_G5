namespace BankerBot
{
    public class ShowBalanceCondition : ICondition<IMessage>
    {
        /// <summary>
        /// Condición que deberá ser cumplida para que ShowBalanceHandler pueda llevar a cabo su acción.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ConditionIsMet(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);
            return data.ConversationState == ConversationState.HandlingRequest && data.Command.ToLower() == "/mostrarbalance";
        }
    }
}