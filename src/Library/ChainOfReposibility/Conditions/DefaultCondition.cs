namespace BankerBot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Condición por defecto.
    /// </summary>
    public class DefaultCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            return true;
        }
    }
}