namespace BankerBot
{
    public class AddExpenseCategoryCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/agregarcategoriadegasto";
        }
    }
}