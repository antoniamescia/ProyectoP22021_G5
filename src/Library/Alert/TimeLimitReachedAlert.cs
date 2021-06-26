using System;

namespace Library
{
    public class TimeLimitReachedAlert : IAlert
    { 
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método SendAlert.
        Cumple con LSP porque el tipo implícito que define la clase puede ser sustiuido por IAlert.
        Cumple con ISP porque solo implementa una interfaz (IAlert).
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad de enviar la alerta.
        Cumple con Polymorphism porque usa el método polimórfico SendAlert.
        */

        /// <summary>
        /// Crea una alerta que será enviada al realizar una transacción si ya pasó el tiempo límite de ahorro y todavía no se alcanzó el objetivo de ahorro máximo.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string SendAlert(Account account)
        {
           if ((account.MaxGoal.TimeLimit - DateTime.Today).TotalDays < 0 && account.Amount < account.MaxGoal.ObjectiveAmount)
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
