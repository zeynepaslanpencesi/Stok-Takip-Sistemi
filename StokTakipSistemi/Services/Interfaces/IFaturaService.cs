using StokTakipSistemi.DTOModels;
using StokTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Services.Interfaces
{
    public interface IFaturaService: IGenericRepository<Fatura>
    {
        Task<FaturaDTO> GetWithRelative(int? id);
        Task<List<FaturaDTO>> GetAllWithRelatives();
        Task<FaturaDüzenleDTO> GetWithSiparisler(int? id);
    }
}
