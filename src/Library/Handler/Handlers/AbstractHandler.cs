namespace Library
{
    /*Cumple con ## SRP ## 
    Cumple con ## EXPERT ##*/
    /// <summary>
    /// Handler abstracto.
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
