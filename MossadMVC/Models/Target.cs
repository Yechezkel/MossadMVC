using System.Drawing;

namespace MossadServer.Models
{
    public class Target : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public string Role { get; set; } = "";
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
       
    }
}
