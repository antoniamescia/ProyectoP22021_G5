using System;

namespace Library
{
    public class MaxSavingsGoalAlert : IAlert
    {
        public string SendAlert(Account account)
        {
            if (account.MaxGoal.ObjectiveAmount - account.Amount <= 100)
            {
             double ammountLeft = account.MaxGoal.ObjectiveAmount - account.Amount;
             string alert = $"¡Wohoo! Te restan ${ammountLeft} para llegar a tu objetivo máximo de ahorro. 🙌";  
             return alert; 
            }
            else
            {
                return null;
            }
        }
    }
}
