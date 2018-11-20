using Microsoft.EntityFrameworkCore;
using StokTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Data
{
    public class StokTakipSistemiDbContext: DbContext
    {
        public StokTakipSistemiDbContext(DbContextOptions<StokTakipSistemiDbContext> options) : base(options)
        {
        }
        public DbSet<Urun> Urun { get; set; }
        public DbSet<UrunTur> UrunTur { get; set; }
        public DbSet<Marka> Marka { get; set; }
        public DbSet<Sehir> Sehir { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<Adres> Adres { get; set; }
        public DbSet<Firma> Firma { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Fatura> Fatura { get; set; }
        public DbSet<Siparis> Siparis { get; set; }         
        public DbSet<UrunSaglayici> UrunSaglayici { get; set; }
        public DbSet<Departman> Departman { get; set; }
        public DbSet<Unvan> Unvan { get; set; }
       
        



    }
}
