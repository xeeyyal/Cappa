using Cappa.Models.Common;

namespace Cappa.Models
{
    public class Setting:BaseEntity
    {
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
