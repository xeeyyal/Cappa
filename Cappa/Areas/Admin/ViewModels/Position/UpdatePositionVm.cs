﻿using System.ComponentModel.DataAnnotations;

namespace Cappa.Areas.Admin.ViewModels
{
    public class UpdatePositionVm
    {
        [Required]
        public string Name { get; set; }
    }
}
