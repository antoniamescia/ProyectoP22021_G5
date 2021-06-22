using System;

namespace Library
{
    public class SavingsGoal
    {
        //Cumple con SRP pues no se halla más de una razón de cambio.
        //Cumple con Expert pues es la experta en la información de los objetivos de ahorro. 
        //Crea instancias de Alert por que, al momento, es la única clase que las utiliza de manera estrecha. Así cumple con Creator.
        public SavingsGoal(double objectiveAmount, Currency currency, DateTime timeLimit)
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
        public DateTime TimeLimit
        {
            get;
            private set;
        }

        // public void SendAlert(IAlert alert)
        // {

        // }
    }
}
