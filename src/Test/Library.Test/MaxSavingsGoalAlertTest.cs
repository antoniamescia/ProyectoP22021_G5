using NUnit.Framework;
using System;

namespace BankerBot.Test
{
    public class MaxSavingsGoalAlertTest
    {
        Currency pesosUruguayos;
        DateTime limit;
        Account itauPesos;

        [SetUp]
        public void Setup()
        {
            this.pesosUruguayos = new Currency("UYU", "U$", 1);
            this.limit = new DateTime(2021, 06, 20); 
            this.itauPesos = new Account("Itau Pesos", Type.CajaDeAhorros, pesosUruguayos, 35990, new SavingsGoal(36000, 25000));  
        }

        [Test]
        public void MaxAlertIsCreatedWhenDifferenceIsLessThan100()
        {
            string expectedAlert = "¡Wohoo! Te restan $10 para llegar a tu objetivo máximo de ahorro. 🙌";
            IAlert maxAlert = new MaxSavingsGoalAlert();
            string actualAlert = maxAlert.SendAlert(itauPesos);

            Assert.AreEqual(expectedAlert, actualAlert);
        }
        
        [Test]
        public void MaxAlertIsNotCreatedWhenDifferenceIsGreatherThan100()
        {
            itauPesos.Transfer(pesosUruguayos, -400, "COMPRAS");
            IAlert maxAlert = new MaxSavingsGoalAlert();
            string actualAlert = maxAlert.SendAlert(itauPesos);

            Assert.AreEqual(null, actualAlert);

        }

        [Test]
        public void MaxAlertIsNotCreatedWhenMaxSavingsGoalIsZero()
        {
            itauPesos.ChangeSavingsGoal(0, 25000);
            IAlert maxAlert = new MaxSavingsGoalAlert();
            string actualAlert = maxAlert.SendAlert(itauPesos);

            Assert.AreEqual(null, actualAlert);            
        }


    }
}