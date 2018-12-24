using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.ViewModels
{
    public class KullaniciVM
    {
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Email { get; set; }
        [StringLength(15, MinimumLength = 8)]
        public string Sifre { get; set; }
        public DateTime Dogum_gunu { get; set; }
        public DateTime Baslangic_Tarihi { get; set; }
    }
}
