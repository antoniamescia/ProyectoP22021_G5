namespace Bankbot
{
    /* Cumple con ## OCP ## ya que se pueden seguir agregando bots sin alterar código.*/
    
    /// <summary>
    /// Implementa un bot que se le pase.
    /// </summary>
    public abstract class AbstractBot : IChannel
    {
        private AbstractHandler<IMessage> Handler;
        protected AbstractBot()
        {
            this.Handler = Configuration.HandlerSetup();
        }
        public abstract void Start();
        public void HandleMessage(IMessage message)
        {
            Handler.Handler(message);
        }
        public void SetChannel(string id, IChannel channel)
        {
            Session.Instance.SetChannel(id, channel);
        }
        public abstract void SendMessage(string id, string message);
        


    }
}