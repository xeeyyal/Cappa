using Cappa.Models.Common;

namespace Cappa.Models
{
    public class Menu:BaseNameableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
