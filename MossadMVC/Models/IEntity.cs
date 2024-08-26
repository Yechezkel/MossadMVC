namespace MossadServer.Models
{
    public interface IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } 
        public int X { get; set; }
        public int Y { get; set; } 
    }
}
