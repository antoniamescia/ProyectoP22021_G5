namespace BankerBot
{
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
        public void HandleMessage(IMessage message)
        {
            Handler.Handler(message);
        }
        public void SetChannel(string id, ICommunicationChannel channel)
        {
            Session.Instance.SetComunicationChannel(id, channel);
        }
        public abstract void SendMessage(string id, string message);

        public abstract void SendPrint(string id, string path);

    }
}