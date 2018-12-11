using AutoMapper;
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
    public class FaturaService: GenericRepository<Fatura>, IFaturaService
    {
        public FaturaService(StokTakipSistemiDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<FaturaDTO>> GetAllWithRelatives()
        {
            var faturalar = await _dbSet.ToListAsync();
            var faturaDTOs = new List<FaturaDTO>();

            foreach (var item in faturalar)
            {
                var urunsaglayici = await _dbContext.UrunSaglayici.FindAsync(item.UrunSaglayiciId);

                var mappedFatura = new FaturaDTO
                {
                    Id = item.Id,
                    Tarih = item.Tarih,
                    UrunSaglayici = urunsaglayici.Adi
                };

                faturaDTOs.Add(mappedFatura);
            }

            return faturaDTOs;
        }

        public async Task<FaturaDTO> GetWithRelative(int? id)
        {
            var fatura = await _dbSet.FindAsync(id);

            if (fatura != null)
            {
                var urunsaglayici = await _dbContext.UrunSaglayici.FindAsync(fatura.UrunSaglayiciId);

                var mappedFatura = new FaturaDTO
                {
                    Id = fatura.Id,
                    Tarih = fatura.Tarih,
                    UrunSaglayici = urunsaglayici.Adi
                };

                return mappedFatura;
            }

            return null;
        }

        public async Task<FaturaDüzenleDTO> GetWithSiparisler(int? id)
        {
            var fatura = await _dbSet.FindAsync(id);

            if (fatura != null)
            {
                var urunsaglayici = await _dbContext.UrunSaglayici.FindAsync(fatura.UrunSaglayiciId);
                var siparisler = await _dbContext.Siparis.Where(o => o.FaturaId == id).ToListAsync();

                var mappedFatura = new FaturaDüzenleDTO
                {
                    Id = fatura.Id,
                    Tarih = fatura.Tarih,
                    UrunSaglayiciId = urunsaglayici.Id,
                    Siparisler = Mapper.Map<ICollection<SiparisDTO>>(siparisler)
                };

                return mappedFatura;
            }

            return null;
        }

       
    }
}
