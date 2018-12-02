using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.DTOModels
{
    public class SiparisSelfDTO
    {
        public int Id { get; set; }
        public int Adet { get; set; }
        public int FaturaId { get; set; }
        public string Urun { get; set; }
        public int UrunId { get; set; }
        public DateTime Tarih { get; set; }
    }
}
