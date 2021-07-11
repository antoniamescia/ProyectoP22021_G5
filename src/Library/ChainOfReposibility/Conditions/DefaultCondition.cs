namespace BankerBot
{
    /// <summary>
    /// Condición que siempre será verdadera.
    /// </summary>
    public class DefaultCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            return true;
        }
    }
}