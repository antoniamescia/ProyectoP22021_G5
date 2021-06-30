using System;
using System.Collections.Generic;

namespace Library
{
    public class CurrencyExchanger
    {
        /*
        Patrones y principios:
        Cumple con el patrón Expert pues es la experta en la información necesaria para realizar las responsabilidades otorgadas.
        Cumple con el patrón Creator pues al usar muy estrechamente instancias de Currency, se encarga de crearlas. 
        Cumple con SRP porque no se identifica más de una razón de cambio.
        Cumple con el patrón Singleton. Al tener un constructor privado nos aseguramos que no puedan crearse instancias de esta clase. La propiedad Instance nos provee un único punto de acceso al convertor.
        */
        private List<Currency> currencyList;
        
        private static CurrencyExchanger instance;
        public static CurrencyExchanger Instance
        {
            get
            {
                if (instance == null) 
                {
                    instance = new CurrencyExchanger();
                }
                return instance;
            }
        }

        private CurrencyExchanger()
        {
            this.currencyList = new List<Currency>() { new Currency("UYU", 1), new Currency("USD", 45.05), new Currency("EUR", 55.03), new Currency("BRL", 9.89) };
        }
        
        
        public IList<Currency> CurrencyList
        {
            get
            {
                return currencyList.AsReadOnly();
            }
        }
        

       
        /// <summary>
        /// Agrega un nuevo tipo de moneda
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rate"></param>
        public void AddCurrency(string type, double rate)
        {
            if (ExistsCurrency(type) == false)
            {
                Currency newCurrency = new Currency(type, rate);
                currencyList.Add(newCurrency);
            }
        }
        
        /// <summary>
        /// Remueve un tipo de moneda
        /// </summary>
        /// <param name="type"></param>
        public void RemoveCurrency(string type)
        {
            foreach (Currency currency in currencyList)
            {
                if (currency.Type == type)
                {
                    currencyList.Remove(currency);
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
            if (initialCurrency.Type != "UYU")
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


    }
}
