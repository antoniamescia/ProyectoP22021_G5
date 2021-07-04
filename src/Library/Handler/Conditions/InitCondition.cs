namespace Library
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Condición inicial.
    /// </summary>
    public class InitCondition : ICondition<UserMessage>
    {
        public bool IsSatisfied(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);
            return data.State == State.Init;
        }
    }
}