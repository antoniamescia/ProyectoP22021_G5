namespace Bankbot
{
    public interface ICondition<IMessage>
    {
        bool IsSatisfied(IMessage request);
    }
}