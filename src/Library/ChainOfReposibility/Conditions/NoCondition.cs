// namespace Bankbot
// {
//     /*Cumple con EXPERT y SRP*/
//     /// <summary>
//     /// Condición por defecto.
//     /// </summary>
//     public class DefaultCondition : ICondition<UserMessage>
//     {
//         public bool IsSatisfied(UserMessage request)
//         {
//             return true;
//         }
//     }
// }

namespace BankerBot
{
    public class NoCondition : ICondition<UserMessage>
    {
        public bool ConditionIsMet(UserMessage request)
        {
           return true;
        }
    }
}