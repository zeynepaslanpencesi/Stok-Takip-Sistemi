using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.DTOModels
{
    public class FaturaDTO
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public string UrunSaglayici { get; set; }
    }
}
