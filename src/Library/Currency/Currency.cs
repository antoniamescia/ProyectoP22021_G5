namespace BankerBot
{
    public class Currency
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public double ConvertionRate { get; set; }

        public Currency(string code, string type, double rate)
        {
            this.Code = code;
            this.Type = type;
            this.ConvertionRate = rate;
        }
    }
}