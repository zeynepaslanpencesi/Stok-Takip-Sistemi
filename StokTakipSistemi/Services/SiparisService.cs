using StokTakipSistemi.Data;
using StokTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Services
{
    public class SiparisService: GenericRepository<Siparis>, ISiparisService
    {
        public SiparisService(StokTakipSistemiDbContext dbContext) : base(dbContext)
        {
        }
    }
}
