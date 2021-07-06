namespace Library
{
    public class DefaultCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
            return true;
        }
    }
}