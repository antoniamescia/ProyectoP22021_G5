namespace BankerBot
{
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio.
        Cumple con LSP porque el tipo implícito que define la clase puede ser sustiuido por IMessage.
        Cumple con ISP porque solo implementa una interfaz (IMessage).
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad asignada.
        */
    public class BotMessage : IMessage
    {
        public string UserID { get; set; }
        public string MessageText { get; set; }
        public BotMessage(string id, string message)
        {
            this.UserID = id;
            this.MessageText = message;
        }
    }
}