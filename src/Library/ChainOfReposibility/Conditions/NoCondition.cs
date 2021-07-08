
namespace Bankbot
{
    public class NoCondition : ICondition<IMessage>
    {
        public bool IsSatisfied(IMessage request)
        {
           return true;
        }
    }
}