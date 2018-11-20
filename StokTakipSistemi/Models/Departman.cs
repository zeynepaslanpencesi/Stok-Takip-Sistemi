using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Models
{
    public class Departman
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Adi { get; set; }
    }
}
