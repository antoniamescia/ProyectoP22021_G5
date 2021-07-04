namespace Library
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Se encarga de dar la posibilidad de convertir al usuario.
    /// </summary>
    public class ConvertionCondition : ICondition<UserMessage>
    {
        public bool IsSatisfied(UserMessage request)
        {
            var data = Session.Instance.GetChatInfo(request.User);
            return data.State == State.HandlingCommand && data.Command.ToLower() == "/convertion";
        }
    }
}