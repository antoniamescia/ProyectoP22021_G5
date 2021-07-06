using System;
using System.Collections.Generic;

namespace BankerBot
{
    public interface IPrinter
    {
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio.
        Cumple con OCP porque permite la introducción de nuevos tipos de impresoras sin modificar el código existente (se agregan como nuevas clases).
        */
        //void PrintAccountBalance(Account account);
        string Print(List<Transaction> list, string path);

        //void PrintSavingsGoal(SavingsGoal savingsGoal);
    }
}
