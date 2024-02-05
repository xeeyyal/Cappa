using System.ComponentModel.DataAnnotations;

namespace Cappa.Areas.Admin.ViewModels
{
    public class UpdateSettingVm
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
