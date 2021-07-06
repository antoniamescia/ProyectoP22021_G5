using System;

namespace BankerBot
{
    //Esta cumple con el patrón Expert ya que es la que posee toda la información sobre Currency.
    //Además también cumple con el patrón SRP por tener una unica razón de cambio.
    

    /// <summary>
    /// Clase correspondiente al tipo de moneda
    /// </summary>
    public class Currency
    {
        public string Type { get; set; }
        public double ExchangeRate { get; set; }

        public Currency(string type, double rate)
        {
            this.Type = type;
            this.ExchangeRate = rate;
        }
    }
}