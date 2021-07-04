namespace Library
{
    /*Cumple con EXPERT y SRP*/

    /// <summary>
    /// Comando para realizar una transacción.
    /// </summary>
    public class TransactionCondition : ICondition<UserMessage>
    {
        public bool IsSatisfied(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);
            return data.State == State.HandlingCommand && data.Command.ToLower() == "/transaction";
        }
    }
}