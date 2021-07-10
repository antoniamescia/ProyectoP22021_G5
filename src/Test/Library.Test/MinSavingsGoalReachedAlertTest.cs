// using NUnit.Framework;
// using System;

// namespace BankerBot.Test
// {
//     public class MinSavingsGoalReachedAlertTest
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
//             this.itauPesos = new Account("Itau Pesos", pesosUruguayos, 25200, max, min); 
//         }

//         [Test]
//         public void MinReachedAlertIsCreatedWhenObjetiveHasBeenReached()
//         {
//             itauPesos.Transfer(pesosUruguayos, -210, "compras");

//             string expectedAlert = "Has pasado tu objetivo mínimo de ahorro. 😮";
//             IAlert minReachedAlert = new MinSavingsGoalReachedAlert();
//             string actualAlert = minReachedAlert.SendAlert(itauPesos);

//             Assert.AreEqual(expectedAlert, actualAlert);
//         }
        
//         [Test]
//         public void MinReachedAlertIsNotCreatedWhenObjetiveHasNotBeenReached()
//         {

//             IAlert minReachedAlert = new MinSavingsGoalReachedAlert();
//             string actualAlert = minReachedAlert.SendAlert(itauPesos);

//             Assert.AreEqual(null, actualAlert);

//         }



//     }
// }