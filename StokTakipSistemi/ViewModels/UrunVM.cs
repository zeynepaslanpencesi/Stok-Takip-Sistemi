using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.ViewModels
{
    public class UrunVM
    {
        public string Adi { get; set; }
        public decimal Fiyat { get; set; }
        public int UrunTurId { get; set; }
        public int MarkaId { get; set; }
        public int Adet { get; set; }
    }
}
