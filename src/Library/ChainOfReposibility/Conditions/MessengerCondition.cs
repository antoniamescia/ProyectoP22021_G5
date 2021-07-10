namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    public class MessengerCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);
            return data.State == State.Messenger && Commands.Instance.Exists(request.MessageText);
        }
    }
}