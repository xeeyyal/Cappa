using System.ComponentModel.DataAnnotations;

namespace Cappa.Areas.Admin.ViewModels
{
    public class UpdateRoomVm
    {
        [Required]
        public string Name { get; set; }
        public string? Image { get; set; }
        public IFormFile? Photo { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
