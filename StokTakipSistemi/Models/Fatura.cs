using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Models
{
    public class Fatura
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public int UrunSaglayiciId { get; set; }
    }
}
