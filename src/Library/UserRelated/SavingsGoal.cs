using System;

// namespace Library
// {
//     public class SavingsGoal
//     {
//         /*
//         Patrones y principios:
//         Cumple con SRP pues no se halla más de una razón de cambio.
//         Cumple con Expert pues es la experta en la información de los objetivos de ahorro.
//         */ 
//         public SavingsGoal(double objectiveAmount, Currency currency, DateTime timeLimit)
//         {
//             this.ObjectiveAmount = objectiveAmount;
//             this.Currency = currency;
//             this.TimeLimit = timeLimit;
//         }
//         public double ObjectiveAmount { get; private set; }
//         public Currency Currency { get; private set; }
//         public DateTime TimeLimit { get; private set; }
//     }
// }

namespace Bankbot
{
    public class SavingsGoal
    {
        public double Max { get; set; }
        public double Min { get; set; }
        public DateTime TimeLimit { get; set; }

        public SavingsGoal(double max, double min)
        {
            this.Max = max;
            this.Min = min;
        }
    }
}