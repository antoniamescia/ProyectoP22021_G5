namespace Bankbot
{
    public class Objective
    {
        public double Max { get; set; }
        public double Min { get; set; }

        public Objective(double max, double min)
        {
            this.Max = max;
            this.Min = min;
        }
    }
}