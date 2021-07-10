
namespace BankerBot
{
    public class NoCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
           return true;
        }
    }
}