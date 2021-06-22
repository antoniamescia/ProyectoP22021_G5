using System;

namespace Library
{
    public class TimeLimitAlert : IAlert
    {
        public string SendAlert(Account account)
        {
           if ((account.MaxGoal.TimeLimit - DateTime.Today).TotalDays <= 7 && account.Balance < account.MaxGoal.ObjectiveAmount)
            {
             double daysLeft = (account.MaxGoal.TimeLimit - DateTime.Today).TotalDays;
             string alert = $"¡Atención! Tienes {daysLeft} días para llegar a tu objetivo máximo de ahorro. 💵🏃🏼";  
             return alert; 
            }
            else
            {
                return null;
            }

        }
    }
}
