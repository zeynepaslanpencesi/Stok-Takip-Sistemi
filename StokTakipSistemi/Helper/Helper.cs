using Microsoft.AspNetCore.Mvc.Rendering;
using StokTakipSistemi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Helper
{
    public class Helper
    {
        private readonly StokTakipSistemiDbContext _dbContext;

        public Helper(StokTakipSistemiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<SelectListItem> GetUrunSelectList(int? id = null)
        {
            IList<SelectListItem> selectList;

            if (id == null)
            {
                selectList = _dbContext.Urun.ToList().
                    Select(p => new SelectListItem() { Text = p.Adi, Value = p.Id.ToString() }).
                    ToList();

                return selectList;
            }

            selectList = new List<SelectListItem>();

            foreach (var product in _dbContext.Urun.ToList())
            {
                if (product.Id == id)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = product.Id.ToString(),
                        Text = product.Adi,
                        Selected = true
                    });
                }
                else
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = product.Id.ToString(),
                        Text = product.Adi
                    });
                }
            }

            return selectList;
        }
    }
}
