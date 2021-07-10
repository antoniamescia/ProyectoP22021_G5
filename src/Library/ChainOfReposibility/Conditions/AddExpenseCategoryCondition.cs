namespace BankerBot
{
    public class AddExpenseCategoryCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/agregarcategoriadegasto";
        }
    }
}