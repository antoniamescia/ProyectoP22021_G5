using System;

namespace Library
{
    public interface IPrinter
    {
        void PrintAccountBalance(Account account);
        void PrintTransactions(Transaction transaction, string limitDate);
        void PrintSavingsGoal(SavingsGoal savingsGoal);
    }
}
