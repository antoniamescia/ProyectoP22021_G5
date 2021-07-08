namespace Bankbot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Da la posibilidad al usuario de crear una cuenta con divisa a elección.
    /// </summary>
    public class CreateAccountCondition : Bankbot.ICondition<IMessage>
    {
        public bool IsSatisfied(IMessage request)
        {
            var data = Session.Instance.GetChat(request.Id);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/crearcuenta";
        }
    }
}