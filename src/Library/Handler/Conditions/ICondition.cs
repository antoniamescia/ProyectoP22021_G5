namespace Library
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Intefaz de la condición.
    /// </summary>
    /// <typeparam name="IMessage"></typeparam>
    public interface ICondition<UserMessage>
    {
        bool IsSatisfied(UserMessage request);
    }
}