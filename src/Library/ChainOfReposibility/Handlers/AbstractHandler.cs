namespace Library
{
    public abstract class AbstractHandler<T>
    {
        protected abstract void handleRequest(UserMessage request);
        private ICondition<UserMessage> condition;
        public AbstractHandler<T> Succesor { get; set; }
        protected AbstractHandler(ICondition<UserMessage> condition)
        {
            this.condition = condition;
        }
        public virtual void Handler(UserMessage request)
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
