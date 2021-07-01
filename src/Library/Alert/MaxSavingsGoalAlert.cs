using System;

namespace BankerBot
{
    public class MaxSavingsGoalAlert : IAlert
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
        /// Crea una alerta que será enviada al realizar una transacción cuando la diferencia entre el objetivo máximo de ahorro y el balance actual es menor o igual a 100.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string SendAlert(Account account)
        {
            if (account.MaxGoal.ObjectiveAmount - account.Amount <= 100 && account.MaxGoal.ObjectiveAmount > 0)
            {
             double ammountLeft = account.MaxGoal.ObjectiveAmount - account.Amount;
             string alert = $"¡Wohoo! Te restan ${ammountLeft} para llegar a tu objetivo máximo de ahorro. 🙌";  
             return alert; 
            }
            else
            {
                return null;
            }
        }
    }
}
