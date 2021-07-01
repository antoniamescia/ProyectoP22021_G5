using NUnit.Framework;
using System;

namespace BankerBot.Test
{
    public class MaxSavingsGoalReachedAlertTest
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
            this.limit = new DateTime(2021, 06, 20);
            this.max = new SavingsGoal(36000, pesosUruguayos, limit);
            this.min = new SavingsGoal(25000, pesosUruguayos, limit);    
            this.itauPesos = new Account("Itau Pesos", pesosUruguayos, 35990, max, min); 
        }

        [Test]
        public void MaxReachedAlertIsCreatedWhenActualAmountIsGreatherThanMaxGoal()
        {
            itauPesos.Transfer(pesosUruguayos, 100, "Compras");
            string expectedAlert = "¡Haz alcanzando tu objetivo de ahorro máximo para la cuenta Itau Pesos! ¡Felicitaciones! 🥳";
            IAlert maxReachedAlert = new MaxSavingsGoalReachedAlert();
            string actualAlert = maxReachedAlert.SendAlert(itauPesos);

            Assert.AreEqual(expectedAlert, actualAlert);
        }
        
        [Test]
        public void MaxReachedAlertIsNotCreatedWhenActualAmountIsLessThanMaxGoal()
        {
            IAlert maxReachedAlert = new MaxSavingsGoalReachedAlert();
            string actualAlert = maxReachedAlert.SendAlert(itauPesos);

            Assert.AreEqual(null, actualAlert);

        }

        [Test]
        public void MaxReachedAlertIsNotCreatedWhenMaxSavingsGoalIsZero()
        {
            itauPesos.ChangeMinGoal(0, pesosUruguayos, limit);
            IAlert maxReachedAlert = new MaxSavingsGoalReachedAlert();
            string actualAlert = maxReachedAlert.SendAlert(itauPesos);

            Assert.AreEqual(null, actualAlert);
            
        }


    }
}