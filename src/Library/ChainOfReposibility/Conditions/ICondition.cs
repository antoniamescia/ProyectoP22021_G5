namespace BankerBot
{
    /*
    Patrones y principios:
    Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en el método ConditionIsMet.
    Cumple con OCP porque permite la introducción de nuevos tipos de condiciones sin modificar el código existente (se agregan como nuevas clases).
    */
    public interface ICondition<IMessage>
    {
        bool ConditionIsMet(IMessage request);
    }
}