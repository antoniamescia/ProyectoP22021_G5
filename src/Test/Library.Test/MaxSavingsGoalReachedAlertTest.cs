using NUnit.Framework;
using System;

namespace BankerBot.Test
{
    public class MaxSavingsGoalReachedAlertTest
    {
        Currency pesosUruguayos;
        DateTime limit;
        Account itauPesos;

        [SetUp]
        public void Setup()
        {
            this.pesosUruguayos = new Currency("UYU", "U$", 1);
            this.limit = new DateTime(2021, 06, 20);

            this.itauPesos = new Account("Itau Pesos", Type.Debito, pesosUruguayos, 35990, new SavingsGoal(36000, 25000)); 
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
            itauPesos.ChangeSavingsGoal(36000, 0);
            IAlert maxReachedAlert = new MaxSavingsGoalReachedAlert();
            string actualAlert = maxReachedAlert.SendAlert(itauPesos);

            Assert.AreEqual(null, actualAlert);
            
        }


    }
}