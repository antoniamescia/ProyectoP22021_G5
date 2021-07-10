namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    public class ExitCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);
            return data.State != State.Messenger && request.Text.ToLower() == "/salir";
        }
    }
}