namespace Bankbot
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/
    /// <summary>
    /// Handler abstracto.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractHandler<T>
    {
        protected abstract void handleRequest(IMessage request);
        public AbstractHandler<T> Succesor { get; set; }
        private ICondition<IMessage> condition;
        protected AbstractHandler(ICondition<IMessage> condition)
        {
            this.condition = condition;
        }
        public virtual void Handler(IMessage request)
        {
            if (this.condition.IsSatisfied(request)) 
            {
                this.handleRequest(request);
                return;
            }
            if (this.Succesor != null) 
            {
                this.Succesor.Handler(request);
            }
        }

        public void AddSuccesor(AbstractHandler<T> handler)
        {
            this.Succesor = handler;
        }
    }
}
