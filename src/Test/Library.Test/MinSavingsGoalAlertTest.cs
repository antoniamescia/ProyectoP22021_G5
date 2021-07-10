using NUnit.Framework;
using System;

namespace BankerBot.Test
{
    public class MinSavingsGoalAlertTest
    {
        Currency pesosUruguayos;
        DateTime limit;
        Account itauPesos;

        [SetUp]
        public void Setup()
        {
            this.pesosUruguayos = new Currency("UYU", "U$", 1);
            this.limit = new DateTime(2021, 06, 20);  
            this.itauPesos = new Account("Itau Pesos", Type.CajaDeAhorros, pesosUruguayos, 25200, new SavingsGoal(36000, 25000)); 
        }

        [Test]
        public void MinAlertIsCreatedWhenDifferenceIsLessThan100()
        {
            itauPesos.Transfer(pesosUruguayos, -150, "compras");

            string expectedAlert = "¡Cuidado! Puedes gastar $50 antes de llegar a tu objetivo mínimo de ahorro. 😱";
            IAlert minAlert = new MinSavingsGoalAlert();
            string actualAlert = minAlert.SendAlert(itauPesos);

            Assert.AreEqual(expectedAlert, actualAlert);
        }
        
        [Test]
        public void AlertIsNotCreatedWhenDifferenceIsGreatherThan100()
        {

            IAlert minAlert = new MinSavingsGoalAlert();
            string actualAlert = minAlert.SendAlert(itauPesos);

            Assert.AreEqual(null, actualAlert);
        }
    }
}