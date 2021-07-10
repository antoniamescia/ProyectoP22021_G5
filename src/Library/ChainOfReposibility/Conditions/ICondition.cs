namespace BankerBot
{
    public interface ICondition<IMessage>
    {
        bool ConditionIsMet(IMessage request);
    }
}