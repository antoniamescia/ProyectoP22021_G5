using System.Collections.Generic;
using System.Text;

namespace BankerBot
{
     /*
        Patrones y principios:
        Cumple con el patrón Expert pues es la experta en la información necesaria para realizar las responsabilidades otorgadas.
        Cumple con el patrón Creator pues al usar muy estrechamente instancias de Currency, se encarga de crearlas. 
        Cumple con SRP porque no se identifica más de una razón de cambio.
        Cumple con el patrón Singleton. Al tener un constructor privado nos aseguramos que no puedan crearse instancias de esta clase. La propiedad Instance nos provee un único punto de acceso al convertor.
        */

    public class CurrencyExchanger
    {
        public List<Currency> CurrencyList { get; set; }
        private static CurrencyExchanger instance;
        public static CurrencyExchanger Instance
        {
            get
            {
                if (instance == null) instance = new CurrencyExchanger();
                return instance;
            }
        }

        private CurrencyExchanger()
        {
            this.CurrencyList = new List<Currency>() { new Currency("UYU", "U$", 1), new Currency("USD", "US$", 43.9), new Currency("EUR", "€ ", 55), new Currency("BRL", "R$", 8.36) };
        }
      

        /// <summary>
        /// Agrega un nuevo tipo de moneda
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rate"></param>
        public void AddCurrency(string code, string type, double rate)
        {
            if (ExistsCurrency(type) == false)
            {
                Currency newCurrency = new Currency(code, type, rate);
                CurrencyList.Add(newCurrency);
            }
        }

        /// <summary>
        /// Remueve un tipo de moneda
        /// </summary>
        /// <param name="type"></param>
        public void RemoveCurrency(string type)
        {
            foreach (Currency currency in CurrencyList)
            {
                if (currency.Type == type)
                {
                    CurrencyList.Remove(currency);
                    return;
                }
            }
        }

        /// <summary>
        /// Realiza la conversión entre los tipo de monedas
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="initialCurrency"></param>
        /// <param name="finalCurrency"></param>
        /// <returns></returns>
         public double Convert(double amount, Currency initialCurrency, Currency finalCurrency)
        {
            if (initialCurrency.Code != "UYU")
            {
                amount = amount * initialCurrency.ExchangeRate;
            }

            return amount / finalCurrency.ExchangeRate;
        }

        /// <summary>
        /// Verifica si existe el tipo de moneda en el listado
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ExistsCurrency(string type)
        {
            foreach (Currency currency in CurrencyList)
            {
                if (currency.Type == type) return true;
            }
            return false;
        }

        /// <summary>
        /// Muestra una lista de los tipo de monedas
        /// </summary>
        /// <returns></returns>
        public string DisplayCurrencyList()
        {
            StringBuilder currencies = new StringBuilder();
            foreach (Currency currency in CurrencyExchanger.Instance.CurrencyList)
            {
                currencies.Append($"{CurrencyExchanger.Instance.CurrencyList.IndexOf(currency) + 1} - {currency.Code}\n");
            }
            return currencies.ToString();
        }
    }
}
