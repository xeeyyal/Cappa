using Cappa.Models;
using System.ComponentModel.DataAnnotations;

namespace Cappa.Areas.Admin.ViewModels
{
    public class CreateEmployeeVm
    {
        [Required]
        public string Name { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        public string? FbLink { get; set; }
        public string? TwLink { get; set; }
        public string? InstaLink { get; set; }
        public string? PinterestLink { get; set; }
    }
}
