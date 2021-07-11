namespace BankerBot
{
    /// <summary>
    /// Clase abstracta con código boilerplate común para todas los handlers concretos.
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public abstract class AbstractHandler<T>
    {
        /*
        Patrones y principios: 
        Cumple con SRP porque solo se identifica una razón de cambio: algún cambio en la lógica del método Handler.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con el patrón Chain of Responsibility.
        */
        
        protected abstract void handleRequest(IMessage request);
        private ICondition<IMessage> condition;
        public AbstractHandler<T> Succesor { get; set; }
        protected AbstractHandler(ICondition<IMessage> condition)
        {
            this.condition = condition;
        }

        /// <summary>
        /// Método que para manejar las peticiones. Si la condición se cumple, desempeña su acción. Si no, pasa la ejecución al siguiente handler luego de comprobar su exitencia.
        /// </summary>
        /// <param name="request"></param>
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

        /// <summary>
        /// Método que establece el siguiente handler de la cadena.
        /// </summary>
        /// <param name="handler"></param>
        public void AddSuccesor(AbstractHandler<T> handler)
        {
            this.Succesor = handler;
        }
    }
}
