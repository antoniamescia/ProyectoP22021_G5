namespace BankerBot
{
    public class AddCurrencyCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/agregarmoneda";
        }
    }
}