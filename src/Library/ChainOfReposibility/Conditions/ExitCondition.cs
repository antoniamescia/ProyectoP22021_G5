namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    public class ExitCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);
            return data.State != State.Messenger && request.MessageText.ToLower() == "/salir";
        }
    }
}