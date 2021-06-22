using System;

namespace Library
{
    public class MinSavingsGoalReachedAlert : IAlert
    {
        public string SendAlert(Account account)
        {
           if (account.Balance <= account.MinGoal.ObjectiveAmount)
            {
             double ammountLeft = account.Balance - account.MinGoal.ObjectiveAmount;
             string alert = $"Has pasado tu objetivo mínimo de ahorro. 😮 ";  
             return alert; 
            }
            else
            {
                return null;
            }

        }
    }
}
