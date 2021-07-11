namespace BankerBot
{
    /*
    Patrones y principios: 
    Cumple con SRP pues solo se identifica una razón de cambio. 
    Cumple con Expert pues es el experto en la información sobre las monedas.
    */
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