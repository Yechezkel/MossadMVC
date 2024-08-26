namespace MossadServer.Models
{
    public class Mission
    {
        public int Id { get; set; }
        public Agent Agent { get; set; }
        public Target Target { get; set; }
        public DateTime? CreationTime { get; set; }= null;//לשנות לזמן התאמה
        public DateTime? ExecutionTime { get; set; }=null;
        public bool Confirmed {  get; set; }=false;
        public double TimeLeft { get; set; } = -1;
    }

    
}
