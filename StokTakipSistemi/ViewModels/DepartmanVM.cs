using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.ViewModels
{
    public class DepartmanVM
    {
        [Required]
        [MaxLength(30)]
        public string Adi { get; set; }
    }
}
