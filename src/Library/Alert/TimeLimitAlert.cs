﻿using System;

namespace Library
{
    public class TimeLimitAlert : IAlert
    {
         /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método SendAlert.
        Cumple con LSP porque el tipo implícito que define la clase puede ser sustiuido por IAlert.
        Cumple con ISP porque solo implementa una interfaz (IAlert).
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad de enviar la alerta.
        Cumple con Polymorphism porque usa el método polimórfico SendAlert.
        */
        public string SendAlert(Account account)
        {
           if ((account.MaxGoal.TimeLimit - DateTime.Today).TotalDays <= 7 && (account.MaxGoal.TimeLimit - DateTime.Today).TotalDays > 0 && account.Amount < account.MaxGoal.ObjectiveAmount)
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
