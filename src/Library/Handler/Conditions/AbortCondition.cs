namespace Library
{
    /*Cumple con EXPERT y SRP*/
    public class AbortCondition : ICondition<UserMessage>
    {
        public bool IsSatisfied(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);
            return data.State != State.Dispatcher && request.MessageText.ToLower() == "/abort";
        }
    }
}