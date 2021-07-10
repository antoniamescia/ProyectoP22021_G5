namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Da la posibilidad al usuario de crear una cuenta con divisa a elección.
    /// </summary>
    public class CreateAccountCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/crearcuenta";
        }
    }
}