using System;

namespace BankerBot
{
    public class SavingsGoal
    {
        /*
        Patrones y principios:
        Cumple con SRP pues no se halla más de una razón de cambio.
        Cumple con Expert pues es la experta en la información de los objetivos de ahorro.
        */ 
        public double Max { get; set; }
        public double Min { get; set; }

        public DateTime TimeLimit {get; set; }


        /// <summary>
        /// Crea los objetivos de ahorro de la cuenta
        /// </summary>
        /// <param name="max"></param>
        /// <param name="min"></param>
        public SavingsGoal(double max, double min)
        {
            this.Max = max;
            this.Min = min;
            //this.TimeLimit = timeLimit;
        }
    }
}