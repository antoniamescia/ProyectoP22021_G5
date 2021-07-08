namespace Bankbot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Se encarga de borrar un usuario si así se desea.
    /// </summary>
    public class DeleteUserCondition : ICondition<IMessage>
    {
        public bool IsSatisfied(IMessage request)
        {
            var data = Session.Instance.GetChat(request.Id);
            return data.State == State.HandlingRequest && data.Command.ToLower() == "/borrarusuario";
        }
    }
}