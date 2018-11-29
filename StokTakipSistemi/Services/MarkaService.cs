using StokTakipSistemi.Data;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Services
{
    public class MarkaService: GenericRepository<Marka>, IMarkaService
    {
        public MarkaService(StokTakipSistemiDbContext dbContext) : base(dbContext)
        {
        }
    }
}
