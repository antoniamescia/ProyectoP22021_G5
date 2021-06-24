using System;
using System.Collections.Generic;

// La clase CurrencyExchanger cumple con patrón Expert y Creator debido a que es experta en información relacionada con el objeto Currency
// y se encarga de crear instancias y almacenarlas. 
// También cumple con el principio de OCP debido a que se encuentra abierta a la extensión y cerrada a la modificación.

namespace Library
{
    public class CurrencyExchanger
    {
        private List<Currency> currencyList;
        
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
            this.currencyList = new List<Currency>() { new Currency("UYU", 1), new Currency("USD", 45.05), new Currency("EUR", 55.03), new Currency("BRL", 9.89) };
        }
        
        //puse que los get devuelvan una IList<T> para poder devolver la lista de modo AsReadOnly
        /*public IList<Currency> CurrencyList
        {
            get
            {
                return currencyList.AsReadOnly();
            }
        }*/
        //faltaria el set de la lista de Currency si es necesario

       
        /// <summary>
        /// Agrega un nuevo tipo de moneda
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rate"></param>
        public void AddCurrency(string type, double rate)
        {
            if (!ExistsCurrency(type))
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
            foreach (var currency in currencyList)
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
            foreach (var t in currencyList)
            {
                if (t.Type == type) return true;
            }
            return false;
        }


    }
}
