using System;

namespace Library
{
    public class SavingsGoal
    {
        /*
        Patrones y principios:
        Cumple con SRP pues no se halla más de una razón de cambio.
        Cumple con Expert pues es la experta en la información de los objetivos de ahorro.
        */ 
        public SavingsGoal(double objectiveAmount, Currency currency, DateTime timeLimit)
        {
            this.ObjectiveAmount = objectiveAmount;
            this.Currency = currency;
            this.TimeLimit = timeLimit;
        }
        public double ObjectiveAmount { get; private set; }
        public Currency Currency { get; private set; }
        public DateTime TimeLimit { get; private set; }
    }
}
