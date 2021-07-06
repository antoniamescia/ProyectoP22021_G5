using NUnit.Framework;
using System;

namespace BankerBot.Test
{
    public class MinSavingsGoalAlertTest
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
            this.itauPesos = new Account("Itau Pesos", pesosUruguayos, 25200, max, min); 
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