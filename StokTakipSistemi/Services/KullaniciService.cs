using StokTakipSistemi.Data;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Services
{
    public class KullaniciService: GenericRepository<Kullanici>, IKullaniciService
    {

        public KullaniciService(StokTakipSistemiDbContext dbContext): base(dbContext)
        {

        }
    }
}
