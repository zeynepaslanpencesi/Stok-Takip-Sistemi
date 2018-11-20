using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }        
        public string Email { get; set; }
        public string Sifre { get; set; }
        public DateTime Dogum_gunu { get; set; }
        public DateTime Baslangic_Tarihi { get; set; }        
        public int UnvanId { get; set; }
        public int DepartmanId { get; set; }
        public int FirmaId { get; set; }
    }
}
