using System.Collections.Generic;

namespace BankerBot
{
    public enum State
    {
        Start,
        Messenger,
        HandlingRequest
    }
    public class Data
    {
        public State State { get; set; }
        public string Command { get; set; }
        public User User { get; set; }
        public Dictionary<string, object> ProvisionalInfo { get; set; }
        public ICommunicationChannel Channel { get; set; }

        public Data()
        {
            this.State = State.Start;
            this.Command = string.Empty;
            this.User = null;
            this.ProvisionalInfo = new Dictionary<string, object>();
            this.Channel = null;
            
        }

        public void ClearOperation()
        {
            this.State = State.Messenger;
            this.ProvisionalInfo.Clear();
            this.Command = string.Empty;
        }
        public T GetDictionaryValue<T>(string key)
        {
            return (T)ProvisionalInfo[key];
        }
    }
}