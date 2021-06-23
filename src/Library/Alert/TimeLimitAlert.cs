using System;

namespace Library
{
    public class TimeLimitReachedAlert : IAlert
    {
        public string SendAlert(Account account)
        {
           if ((account.MaxGoal.TimeLimit - DateTime.Today).TotalDays < 0 && account.Balance < account.MaxGoal.ObjectiveAmount)
            {
             double daysLeft = ((account.MaxGoal.TimeLimit - DateTime.Today).TotalDays) * -1;
             string alert = $"¡Atención! Han pasado {daysLeft} días de tu tiempo límite de ahorro.";  
             return alert; 
            }
            else
            {
                return null;
            }

        }
    }
}
