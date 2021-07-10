namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Interactúa para que se pueda crear un nuevo usuario no existente.
    /// </summary>
    public class CreateUserCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.UserID);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/crearusuario";
        }
    }
}