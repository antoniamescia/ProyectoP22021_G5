namespace Bankbot
{
    /*Cumple con EXPERT y SRP*/
    /// <summary>
    /// Condición por defecto.
    /// </summary>
    public class DefaultCondition : ICondition<IMessage>
    {
        public bool IsSatisfied(IMessage request)
        {
            return true;
        }
    }
}