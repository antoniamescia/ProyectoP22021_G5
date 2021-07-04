namespace Library
{
    /*Cumple con EXPERT y SRP*/

    /// <summary>
    /// Condición para loguearse.
    /// </summary>
    public class LoginCondition : ICondition<UserMessage>
    {
        public bool IsSatisfied(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);
            return data.State == State.HandlingCommand && data.Command.ToLower() == "/login";
        }
    }
}