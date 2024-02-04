using Cappa.Models;
using System.ComponentModel.DataAnnotations;

namespace Cappa.Areas.Admin.ViewModels
{
    public class UpdateEmployeeVm
    {
        public string Name { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public string? Image { get; set; }
        public IFormFile? Photo { get; set; }
        public string? FbLink { get; set; }
        public string? TwLink { get; set; }
        public string? InstaLink { get; set; }
        public string? PinterestLink { get; set; }
    }
}

