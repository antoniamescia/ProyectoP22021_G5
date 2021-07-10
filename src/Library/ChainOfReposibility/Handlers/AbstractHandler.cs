namespace BankerBot
{

    public abstract class AbstractHandler<T>
    {
        protected abstract void handleRequest(IMessage request);
        private ICondition<IMessage> condition;
        public AbstractHandler<T> Succesor { get; set; }
        protected AbstractHandler(ICondition<IMessage> condition)
        {
            this.condition = condition;
        }

        public virtual void Handler(IMessage request)
        {
            if (this.condition.ConditionIsMet(request)) 
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
