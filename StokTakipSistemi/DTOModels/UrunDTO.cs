using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.DTOModels
{
    public class UrunDTO
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public decimal Fiyat { get; set; }
        public string UrunTur { get; set; }
        public string Marka { get; set; }
        public int Adet { get; set; }
    }
}
