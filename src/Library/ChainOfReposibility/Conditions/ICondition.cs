namespace BankerBot
{
    public interface ICondition<UserMessage>
    {
        bool ConditionIsMet(UserMessage request);
    }
}