using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.ViewModels
{
    public class FaturaVM
    {
        public DateTime Tarih { get; set; }
        public int UrunSaglayiciId { get; set; }
        public ICollection<SiparisVM> Siparisler { get; set; }
    }
}
