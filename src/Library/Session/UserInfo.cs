using System.Collections.Generic;

namespace BankerBot
{
    /// <summary>
    /// Tipo enumerado que define estados constantes de la conversación.
    /// </summary>
    public enum ConversationState
    {
        Start,
        Messenger,
        HandlingRequest
    }
    public class UserInfo
    {
        /*
        Patrones y principios:
        Cumple con SRP pues no se identifica más de una razón de cambio. 
        Cumple con Expert pues el experto en la informació necesaria para llevar a cabo las responsabilidades asignadas. 
        */
        public ConversationState ConversationState { get; set; }
        public string Command { get; set; }
        public EndUser User { get; set; }
        public Dictionary<string, object> ProvisionalInfo { get; set; }
        public ICommunicationChannel ComunicationChannel { get; set; }

        /// <summary>
        /// Constructor de UserInfo
        /// </summary>
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