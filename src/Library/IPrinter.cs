using System;

namespace Library
{
    public interface IPrinter
    {
        //Cumple con el patrón OCP porque permite agregar nuevas impresoras sin necesidad de modificar el código existente. 
        //Cumple con SRP pues no se identifica más de una razón de cambio.
        void PrintAccountBalance(Account account);
        void PrintTransactions(Transaction transaction, string limitDate);
        void PrintSavingsGoal(SavingsGoal savingsGoal);
    }
}
