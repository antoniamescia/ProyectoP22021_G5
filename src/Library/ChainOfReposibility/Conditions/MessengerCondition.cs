namespace Bankbot
{
    /*Cumple con EXPERT y SRP*/
    public class MessengerCondition : ICondition<IMessage>
    {
        public bool IsSatisfied(IMessage request)
        {
            var data = Session.Instance.GetChat(request.Id);
            return data.State == State.Messenger && Commands.Instance.CommandExist(request.Text);
        }
    }
}