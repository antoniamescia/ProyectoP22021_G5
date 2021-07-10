namespace BankerBot
{
    public class ChangeAccountObjectiveCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/cambiarobjetivo";
        }
    }
}