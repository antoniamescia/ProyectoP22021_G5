namespace BankerBot
{
    public class TransactionCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/transaccion";
        }
    }
}