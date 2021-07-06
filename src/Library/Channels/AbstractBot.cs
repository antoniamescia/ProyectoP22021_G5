namespace Library
{
    public abstract class AbstractBot : IComunicationChannel
    {
         private AbstractHandler<UserMessage> Handler;
        protected AbstractBot()
        {
            this.Handler = Configuration.ChainOfResponsibility();
        }
         public void SetComunicationChannel(string id, IComunicationChannel channel)
         {
            Session.Instance.SetComunicationChannel(id, channel);
         }
        public abstract void SendMessage(string id, string message);
        public abstract void SendFile(string id, string path);
        public void ManageMessage(UserMessage message)
        {
            Handler.Handler(message);
        }
        public abstract void StartCommunication();
    }
}