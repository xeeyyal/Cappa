using Cappa.Models.Common;

namespace Cappa.Models
{
    public class Room:BaseNameableEntity
    {
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}

