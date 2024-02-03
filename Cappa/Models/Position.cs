using Cappa.Models.Common;

namespace Cappa.Models
{
    public class Position:BaseNameableEntity
    {
        public List<Employee>? Employees { get; set; }
    }
}
