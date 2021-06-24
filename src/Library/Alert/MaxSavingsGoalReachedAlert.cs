using System;

namespace Library
{
    public class MaxSavingsGoalReachedAlert : IAlert
    {
        public string SendAlert(Account account)
        {
            if (account.MaxGoal.ObjectiveAmount <= account.Amount)
            {
                string alert = $"¡Haz alcanzando tu objetivo de ahorro máximo para la cuenta {account.Name}! ¡Felicitaciones! 🥳";  
                return alert; 
            }
            else
            {
                return null;
            }
        }
    }
}
