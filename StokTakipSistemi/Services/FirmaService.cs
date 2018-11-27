using StokTakipSistemi.Data;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Services
{
    public class FirmaService: GenericRepository<Firma>, IFirmaService
    {
        public FirmaService(StokTakipSistemiDbContext dbContext) : base(dbContext)
        {
        }
    }
}
