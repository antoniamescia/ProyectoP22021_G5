namespace Library
{
    public interface ICondition<UserMessage>
    {
        bool ConditionIsMet(UserMessage request);
    }
}