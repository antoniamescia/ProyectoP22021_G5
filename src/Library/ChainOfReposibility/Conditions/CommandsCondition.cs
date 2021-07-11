namespace BankerBot
{
    public class CommandsCondition : ICondition<IMessage>
    {
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método ConditionIsMet.
        Cumple con LSP porque el tipo implícito que define la clase puede ser sustiuido por ICondition.
        Cumple con ISP porque solo implementa una interfaz (ICondition).
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa el método polimórfico ConditionIsMet.
        */
        public bool ConditionIsMet(IMessage request)
        {
            UserInfo data = Session.Instance.GetChatInfo(request.UserID);
            return data.ConversationState == ConversationState.HandlingRequest && data.Command.ToLower() == "/comandos";
        }
    }
}