using Cappa.Models.Common;

namespace Cappa.Models
{
    public class Service:BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
