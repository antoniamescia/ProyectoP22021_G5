using System;

namespace Library 
{
    /*
    Patrones y principios:
    Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en el método SendAlert.
    Cumple con OCP porque permite la introducción de nuevos tipos de alertas sin modificar el código existente (se agregan como nuevas clases).
    */
    public interface IAlert
    {
        string SendAlert(Account account);
    }
}