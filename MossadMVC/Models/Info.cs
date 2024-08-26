namespace MossadMVC.Models
{
    public class Info
    {
        public int SumActiveAgents { get; set; } = 0;
        public int SumTotalAgents { get; set; }= 0;
        public int SumActiveTargets { get; set; } = 0;
        public int SumTotalTargets { get; set; } = 0;
        public double ToPairRatio { get; set; } = -1;
        public double TotalRatio { get; set; }= -1;
    }
}
