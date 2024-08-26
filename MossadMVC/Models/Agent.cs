using System.Drawing;

namespace MossadServer.Models
{
    public class Agent: IEntity
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; } = null;
        public bool IsActive {  get; set; } = false;
        public int SumDoneMission { get; set; }
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;

        public int ActiveMissionId { get; set; } = -1;

    }
}
