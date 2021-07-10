using System;
using System.Collections.Generic;

namespace BankerBot
{
    /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio.
        Cumple con OCP porque permite la introducción de nuevos tipos de impresoras sin modificar el código existente (se agregan como nuevas clases).
        */
    public interface IPrinter
    {
        string Print(List<Transaction> list, string path);
    }
}
