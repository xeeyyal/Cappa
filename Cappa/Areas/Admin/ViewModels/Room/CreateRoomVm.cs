using System.ComponentModel.DataAnnotations;

namespace Cappa.Areas.Admin.ViewModels
{
    public class CreateRoomVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
