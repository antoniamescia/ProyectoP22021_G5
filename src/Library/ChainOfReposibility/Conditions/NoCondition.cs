
namespace Library
{
    public class NoCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
           return true;
        }
    }
}