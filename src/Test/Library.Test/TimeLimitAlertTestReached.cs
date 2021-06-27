using NUnit.Framework;
using System;

namespace Library.Test
{
    public class TimeLimitReachedAlertTest
    {
        Currency pesosUruguayos;
        DateTime limit;
        SavingsGoal max;
        SavingsGoal min;
        Account itauPesos;

        [SetUp]
        public void Setup()
        {
            this.pesosUruguayos = new Currency("UYU", 1);
            this.limit = new DateTime(2021, 06, 10);
            this.max = new SavingsGoal(36000, pesosUruguayos, limit);
            this.min = new SavingsGoal(25000, pesosUruguayos, limit);    
            this.itauPesos = new Account("Itau Pesos", pesosUruguayos, 35900, max, min); 
        }

        [Test]
        public void TimeLimitReachedAlertIsCreatedWhenDateHasBeenReached()
        {
            string expectedAlert = "¡Atención! Han pasado 17 días de tu tiempo límite de ahorro.";
            IAlert timeLimitReachedAlert = new TimeLimitReachedAlert();
            string actualAlert = timeLimitReachedAlert.SendAlert(itauPesos);

            Assert.AreEqual(expectedAlert, actualAlert);
        }
        
        [Test]
        public void TimeLimitReachedAlertIsNotCreatedWhenDateHasNotBeenReached()
        {
            DateTime newLimit = new DateTime(2021, 08, 27);
            itauPesos.ChangeMaxGoal(36000, pesosUruguayos, newLimit);
            IAlert timeLimitReachedAlert = new TimeLimitReachedAlert();
            string actualAlert = timeLimitReachedAlert.SendAlert(itauPesos);

            Assert.AreEqual(null, actualAlert);

        }

        [Test]
        public void TimeLimitReachedAlertIsNotCreatedWhenLimitDateIsToday()
        {
            itauPesos.ChangeMaxGoal(36000, pesosUruguayos, DateTime.Today);
            IAlert timeLimitReachedAlert = new TimeLimitReachedAlert();
            string actualAlert = timeLimitReachedAlert.SendAlert(itauPesos);

            Assert.AreEqual(null, actualAlert);

        }

        [Test]
        public void TimeLimitAlertIsNotCreatedWhenGoalHasAlreadyBeenReached()
        {
            itauPesos.Transfer(pesosUruguayos, 600, "Sueldo");
            IAlert timeLimitReachedAlert = new TimeLimitReachedAlert();
            string actualAlert = timeLimitReachedAlert.SendAlert(itauPesos);

            Assert.AreEqual(null, actualAlert);

        }


    }
}
