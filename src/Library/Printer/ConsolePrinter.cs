using System;
using System.Collections.Generic;

namespace BankerBot
{
    public class ConsolePrinter : IPrinter
    {
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio.
        Cumple con LSP porque el tipo implícito que define la clase puede ser sustiuido por IPrinter.
        Cumple con ISP porque solo implementa una interfaz (IPrinter).
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con las responsabilidades otorgadas.
        Cumple con Polymorphism porque usa los métodos polimórficos PrintAccountBalance, PrintTransactions y PrintSavingsGoal.
        */
        public string Print(List<Transaction> list, string path)
        {       
            return null;
        }
    }
}
