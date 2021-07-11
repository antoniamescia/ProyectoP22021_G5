using System.Collections.Generic;

namespace BankerBot
{
    public enum ConversationState
    {
        Start,
        Messenger,
        HandlingRequest
    }
    public class UserInfo
    {
        public ConversationState ConversationState { get; set; }
        public string Command { get; set; }
        public EndUser User { get; set; }
        public Dictionary<string, object> ProvisionalInfo { get; set; }
        public ICommunicationChannel ComunicationChannel { get; set; }

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