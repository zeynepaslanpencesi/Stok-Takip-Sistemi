using Microsoft.EntityFrameworkCore;
using StokTakipSistemi.Data;
using StokTakipSistemi.DTOModels;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Services
{
    public class UrunService: GenericRepository<Urun>, IUrunService
    {
        public UrunService(StokTakipSistemiDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UrunDTO> GetWithRelative(int? id)
        {
            var urun = await _dbSet.FindAsync(id);

            if (urun != null)
            {
                var urunTur = await _dbContext.UrunTur.FindAsync(urun.UrunTurId);
                var marka = await _dbContext.Marka.FindAsync(urun.MarkaId);

                var mappedUrun = new UrunDTO
                {
                    Id = urun.Id,
                    Adi = urun.Adi,
                    Fiyat = urun.Fiyat,
                    UrunTur = urunTur.Adi,
                    Marka = marka.Adi
                };

                return mappedUrun;
            }

            return null;
        }

        public async Task<List<UrunDTO>> GetAllWithRelatives()
        {
            var uruns = await _dbSet.ToListAsync();
            var urunDTOS = new List<UrunDTO>();

            foreach (var item in uruns)
            {
                var urunTur = await _dbContext.UrunTur.FindAsync(item.UrunTurId);
                var marka = await _dbContext.Marka.FindAsync(item.MarkaId);

                var mappedUrun = new UrunDTO
                {
                    Id = item.Id,
                    Adi = item.Adi,
                    Fiyat = item.Fiyat,
                    UrunTur = urunTur.Adi,
                    Marka = marka.Adi
                };

                urunDTOS.Add(mappedUrun);
            }

            return urunDTOS;
        }
    }
}
