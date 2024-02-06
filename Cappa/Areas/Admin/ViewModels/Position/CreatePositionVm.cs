using System.ComponentModel.DataAnnotations;

namespace Cappa.Areas.Admin.ViewModels
{
    public class CreatePositionVm
    {
        [Required]
        public string Name { get; set; }
    }
}
