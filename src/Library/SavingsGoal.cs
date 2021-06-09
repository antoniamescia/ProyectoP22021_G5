using System;

namespace Library
{
    public class SavingsGoal
    {
        public SavingsGoal(double objectiveAmount, Currency currency, string timeLimit)
        {
            this.ObjectiveAmount = objectiveAmount;
            this.Currency = currency;
            this.TimeLimit = timeLimit;
        }
        public double ObjectiveAmount
        {
            get;
            private set;
        }
        public Currency Currency
        {
            get;
            private set;
        }
        public double ActualSavedAmount
        {
            get;
            private set;
        }
        public string TimeLimit
        {
            get;
            private set;
        }

        public void SendAlert(Alert alert)
        {

        }
    }
}
