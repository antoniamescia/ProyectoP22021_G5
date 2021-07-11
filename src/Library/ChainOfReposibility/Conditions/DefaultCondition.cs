namespace BankerBot
{
    /// <summary>
    /// Condición que por "default" que siempre se cumple, y al cumplirse le hace saber al usuario que no entendió la petición. Por ello, su respectivo handler DefaultHandler se encuentra al final de la cadena de responsabilidad.
    /// </summary>
    
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método ConditionIsMet.
        Cumple con LSP porque el tipo implícito que define la clase puede ser sustiuido por ICondition.
        Cumple con ISP porque solo implementa una interfaz (ICondition).
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa el método polimórfico ConditionIsMet.
        */
    public class DefaultCondition : ICondition<IMessage>
    {
        public bool ConditionIsMet(IMessage request)
        {
            return true;
        }
    }
}