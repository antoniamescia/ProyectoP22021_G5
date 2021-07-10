namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Se encarga de borrar un usuario si así se desea.
    /// </summary>
    public class DeleteUserCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            Data data = Session.Instance.GetChat(request.Id);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/borrarusuario";
        }
    }
}