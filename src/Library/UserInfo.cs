using System;
using System.Collections.Generic;

namespace Library
{
    public enum State
    {
        Init,
        Dispatcher,
        HandlingCommand
    }

    /// <summary>
    /// Clase correspondiente al usuario que está hablandole al bot en determinado momento.
    /// </summary>
    public class UserInfo
    {
        public State State { get; set; }
        public string Command { get; set; }
        public EndUser User { get; set; }   
        public IComunicationChannel ComunicationChannel { get; set; }
        
        public UserInfo()
        {
            this.State = State.Init;
            this.Command = string.Empty;
            this.User = null;
            //this.Temp = new Dictionary<string, object>();
            this.ComunicationChannel = null;
        }

        public void ClearOperation()
        {
            this.State = State.Dispatcher;
            //this.Temp.Clear();
            this.Command = string.Empty;
        }
        // public T GetDictionaryValue<T>(string key)
        // {
        //     return (T)Temp[key];
        // }
    }
}