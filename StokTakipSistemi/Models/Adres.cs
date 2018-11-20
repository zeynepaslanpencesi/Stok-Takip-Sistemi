using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Models
{
    public class Adres
    {
        public int Id { get; set; }
        public string AdresText { get; set; }
        public int SehirId { get; set; }
        public int IlceId { get; set; }
    }
}
