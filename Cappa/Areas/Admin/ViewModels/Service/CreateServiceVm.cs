using System.ComponentModel.DataAnnotations;

namespace Cappa.Areas.Admin.ViewModels
{
    public class CreateServiceVm
    {
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
