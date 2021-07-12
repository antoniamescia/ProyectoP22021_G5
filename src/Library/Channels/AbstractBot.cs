namespace BankerBot
{
        /*
        Cumple con SRP porque solo se identifica una razón de cambio.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa los métodos polimórficos StartCommunication, SendMessage y SendPrint.
        */
    /// <summary>
    /// Bot abstracto que del cual heredarán todos los bots concretos.
    /// </summary>
    public abstract class AbstractBot : ICommunicationChannel
    {
        private AbstractHandler<IMessage> Handler;
        protected AbstractBot()
        {
            this.Handler = Configuration.HandlerSetup();
        }
        public abstract void StartCommunication();
        public void SetChannel(string id, ICommunicationChannel channel)
        {
            Session.Instance.SetComunicationChannel(id, channel);
        }
        public abstract void SendMessage(string id, string message);

        public abstract void SendPrint(string id, string path);
        public void HandleMessage(IMessage message)
        {
            Handler.Handler(message);
        }
    }
}