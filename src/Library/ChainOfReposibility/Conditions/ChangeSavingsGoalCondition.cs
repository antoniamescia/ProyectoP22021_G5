namespace Bankbot
{
    public class ChangeAccountObjectiveCondition : ICondition<IMessage>
    {
        public bool IsSatisfied(IMessage request)
        {
            var data = Session.Instance.GetChat(request.Id);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/cambiarobjetivo";
        }
    }
}