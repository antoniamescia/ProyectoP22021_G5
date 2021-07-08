// using NUnit.Framework;
// using System;

// namespace Bankbot.Test
// {
//     public class TimeLimitAlertTest
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
//             this.limit = DateTime.Today.AddDays(3);
//             this.max = new SavingsGoal(36000, pesosUruguayos, limit);
//             this.min = new SavingsGoal(25000, pesosUruguayos, limit);    
//             this.itauPesos = new Account("Itau Pesos", pesosUruguayos, 35990, max, min); 
//         }

//         [Test]
//         public void TimeLimitAlertIsCreatedWhenDifferenceIsLessThanSevenDays()
//         {
//             string expectedAlert = "¡Atención! Tienes 3 días para llegar a tu objetivo máximo de ahorro. 💵🏃🏼";
//             IAlert timeLimitAlert = new TimeLimitAlert();
//             string actualAlert = timeLimitAlert.SendAlert(itauPesos);

//             Assert.AreEqual(expectedAlert, actualAlert);
//         }
        
//         [Test]
//         public void TimeLimitAlertIsNotCreatedWhenDifferenceIsMoreThanSevenDays()
//         {
//             DateTime newLimit = DateTime.Today.AddDays(10);
//             itauPesos.ChangeMaxGoal(36000, pesosUruguayos, newLimit);
//             IAlert timeLimitAlert = new TimeLimitAlert();
//             string actualAlert = timeLimitAlert.SendAlert(itauPesos);

//             Assert.AreEqual(null, actualAlert);

//         }

//         [Test]
//         public void TimeLimitAlertIsNotCreatedWhenDifferenceIsLessThanZeroDays()
//         {
//             DateTime newLimit = new DateTime(2021, 06, 24);
//             itauPesos.ChangeMaxGoal(36000, pesosUruguayos, newLimit);
//             IAlert timeLimitAlert = new TimeLimitAlert();
//             string actualAlert = timeLimitAlert.SendAlert(itauPesos);

//             Assert.AreEqual(null, actualAlert);

//         }

//         [Test]
//         public void TimeLimitAlertIsNotCreatedWhenGoalHasAlreadyBeenReached()
//         {
//             itauPesos.Transfer(pesosUruguayos, 600, "Sueldo");
//             IAlert timeLimitAlert = new TimeLimitAlert();
//             string actualAlert = timeLimitAlert.SendAlert(itauPesos);

//             Assert.AreEqual(null, actualAlert);

//         }


//     }
// }