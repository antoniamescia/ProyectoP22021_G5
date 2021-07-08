using System.Collections.Generic;

namespace Bankbot
{
    public enum State
    {
        Start,
        Messenger,
        HandlingRequest
    }
    /// <summary>
    /// Clase correspondiente al usuario que está hablandole al bot en determinado momento.
    /// </summary>
    public class Data
    {
        public State State { get; set; }
        public string Command { get; set; }
        public User User { get; set; }
        public Dictionary<string, object> Temp { get; set; }
        public IChannel Channel { get; set; }
        

        /// <summary>
        /// Crea objetos correspondientes a cada usuario.
        /// </summary>
        public Data()
        {
            this.State = State.Start;
            this.Command = string.Empty;
            this.User = null;
            this.Temp = new Dictionary<string, object>();
            this.Channel = null;
            
        }

        public void ClearOperation()
        {
            this.State = State.Messenger;
            this.Temp.Clear();
            this.Command = string.Empty;
        }
        public T GetDictionaryValue<T>(string key)
        {
            return (T)Temp[key];
        }
    }
}