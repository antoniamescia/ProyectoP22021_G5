namespace Bankbot
{
    /*Cumple con EXPERT y SRP*/
    public class AbortCondition : ICondition<IMessage>
    {
        public bool IsSatisfied(IMessage request)
        {
            var data = Session.Instance.GetChat(request.Id);
            return data.State != State.Messenger && request.Text.ToLower() == "/salir";
        }
    }
}