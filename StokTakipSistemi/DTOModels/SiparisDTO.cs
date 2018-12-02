using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.DTOModels
{
    public class SiparisDTO
    {
        public int Id { get; set; }
        public int Adet { get; set; }
        public int FaturaEditDTOId { get; set; }
        public int UrunId { get; set; }
    }
}
