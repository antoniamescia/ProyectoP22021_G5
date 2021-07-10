// using NUnit.Framework;
// using System;

// namespace BankerBot.Test
// {
//     public class MaxSavingsGoalAlertTest
//     {
//         Currency pesosUruguayos;
//         DateTime limit;
//         SavingsGoal max;
//         SavingsGoal min;
//         Account itauPesos;

//         [SetUp]
//         public void Setup()
//         {
//             this.pesosUruguayos = new Currency("UYU", 1);
//             this.limit = new DateTime(2021, 06, 20);
//             this.max = new SavingsGoal(36000, pesosUruguayos, limit);
//             this.min = new SavingsGoal(25000, pesosUruguayos, limit);    
//             this.itauPesos = new Account("Itau Pesos", pesosUruguayos, 35990, max, min);  
//         }

//         [Test]
//         public void MaxAlertIsCreatedWhenDifferenceIsLessThan100()
//         {
//             string expectedAlert = "¡Wohoo! Te restan $10 para llegar a tu objetivo máximo de ahorro. 🙌";
//             IAlert maxAlert = new MaxSavingsGoalAlert();
//             string actualAlert = maxAlert.SendAlert(itauPesos);

//             Assert.AreEqual(expectedAlert, actualAlert);
//         }
        
//         [Test]
//         public void MaxAlertIsNotCreatedWhenDifferenceIsGreatherThan100()
//         {
//             itauPesos.Transfer(pesosUruguayos, -400, "COMPRAS");
//             IAlert maxAlert = new MaxSavingsGoalAlert();
//             string actualAlert = maxAlert.SendAlert(itauPesos);

//             Assert.AreEqual(null, actualAlert);

//         }

//         [Test]
//         public void MaxAlertIsNotCreatedWhenMaxSavingsGoalIsZero()
//         {
//             itauPesos.ChangeMaxGoal(0, pesosUruguayos, limit);
//             IAlert maxAlert = new MaxSavingsGoalAlert();
//             string actualAlert = maxAlert.SendAlert(itauPesos);

//             Assert.AreEqual(null, actualAlert);
            
//         }


//     }
// }