using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Models
{
    public class Siparis
    {
        public int Id { get; set; }
        public int Adet { get; set; }
        public int FaturaId { get; set; }
        public int UrunId { get; set; }
    }
}
