namespace Library
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Interactúa para que se pueda crear un nuevo usuario no existente.
    /// </summary>
    public class CreateUserCondition : ICondition<UserMessage>
    {
        public bool IsSatisfied(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);
            return data.State == State.HandlingCommand && data.Command.ToLower() == "/createuser";
        }
    }
}