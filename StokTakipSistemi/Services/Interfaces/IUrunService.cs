using StokTakipSistemi.DTOModels;
using StokTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Services.Interfaces
{
    public interface IUrunService: IGenericRepository<Urun>
    {
        Task<UrunDTO> GetWithRelative(int? id);
        Task<List<UrunDTO>> GetAllWithRelatives();
    }
}
