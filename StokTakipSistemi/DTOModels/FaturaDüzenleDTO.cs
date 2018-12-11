using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.DTOModels
{
    public class FaturaDüzenleDTO
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public int UrunSaglayiciId { get; set; }
        public ICollection<SiparisDTO> Orders { get; set; }
    }
}
