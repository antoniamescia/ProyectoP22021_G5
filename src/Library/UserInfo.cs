using System;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Tipo enumerado. Conjunto de constantes de valores fijos que determinan el estado actual de la conversación.
    /// 
    /// </summary>
    public enum ConversationState
    {
        Start,
        Messenger,
        HandlingRequest
    }

    /// <summary>
    /// Clase correspondiente al usuario que está hablandole al bot en determinado momento.
    /// </summary>
    public class UserInfo
    {
        public ConversationState ConversationState { get; set; }
        public string Command { get; set; }
        public EndUser User { get; set; }   
        public Dictionary<string, object> ProvisionalInfo { get; set; }
        public IComunicationChannel ComunicationChannel { get; set; }
        
        public UserInfo()
        {
            this.ConversationState = ConversationState.Start;
            this.Command = string.Empty;
            this.User = null;
            this.ProvisionalInfo = new Dictionary<string, object>();
            this.ComunicationChannel = null;
        }

        public void ClearOperation()
        {
            this.ConversationState = ConversationState.Messenger;
            this.ProvisionalInfo.Clear();
            this.Command = string.Empty;
        }
        public T GetDictionaryValue<T>(string key)
        {
            return (T)ProvisionalInfo[key];
        }
    }
}