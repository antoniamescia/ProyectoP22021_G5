using System;

namespace Library
{
    public class MinSavingsGoalAlert : IAlert
    {
        public string SendAlert(Account account)
        {
           if (account.Amount - account.MinGoal.ObjectiveAmount <= 100)
            {
             double ammountLeft = account.Amount - account.MinGoal.ObjectiveAmount;
             string alert = $"¡Cuidado! Puedes gastar ${ammountLeft} antes de llegar a tu objetivo mínimo de ahorro. 😱";  
             return alert; 
            }
            else
            {
                return null;
            }

        }
    }
}
