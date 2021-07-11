namespace BankerBot
{
    //Esta cumple con el patrón Expert ya que es la que posee toda la información sobre Currency.
    //Además también cumple con el patrón SRP por tener una unica razón de cambio.
    
    public class Currency
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public double ExchangeRate { get; set; }

        /// <summary>
        /// Constructor de Currency
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <param name="rate"></param>
        public Currency(string code, string type, double rate)
        {
            this.Code = code;
            this.Type = type;
            this.ExchangeRate = rate;
        }
    }
}