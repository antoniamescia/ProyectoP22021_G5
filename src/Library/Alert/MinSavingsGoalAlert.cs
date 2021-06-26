using System;

namespace Library
{
    public class MinSavingsGoalAlert : IAlert
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
        /// Crea una alerta que será enviada al realizar una transacción cuando la diferencia entre el balance actual y el objetivo mínimo de ahorro sea menor o igual a 100.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string SendAlert(Account account)
        {
           if (account.Amount - account.MinGoal.ObjectiveAmount <= 100)
            {
             double ammountLeft = account.Amount - account.MinGoal.ObjectiveAmount;
             string alert = $"¡Cuidado! Puedes gastar ${ammountLeft} antes de llegar a tu objetivo mínimo de ahorro. 😱";  
             return alert; 
            }
            else
            {
                return null;
            }

        }
    }
}
