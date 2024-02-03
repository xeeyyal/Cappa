using Cappa.Models.Common;

namespace Cappa.Models
{
    public class Employee:BaseNameableEntity
    {
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public string? Image { get; set; }
        public string? FbLink { get; set; }
        public string? TwLink { get; set; }
        public string? InstaLink { get; set; }
        public string? PinterestLink { get; set; }
    }
}
