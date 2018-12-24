using Microsoft.AspNetCore.Mvc.Rendering;
using StokTakipSistemi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakipSistemi.Helpers
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

            foreach (var urun in _dbContext.Urun.ToList())
            {
                if (urun.Id == id)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = urun.Id.ToString(),
                        Text = urun.Adi,
                        Selected = true
                    });
                }
                else
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = urun.Id.ToString(),
                        Text = urun.Adi
                    });
                }
            }

            return selectList;
        }

        public IList<SelectListItem> GetUrunTurSelectList(int? id = null)
        {
            IList<SelectListItem> selectList;

            if (id == null)
            {
                selectList = _dbContext.UrunTur.ToList().
                    Select(t => new SelectListItem() { Text = t.Adi, Value = t.Id.ToString() }).
                    ToList();

                return selectList;
            }

            selectList = new List<SelectListItem>();

            foreach (var urunTur in _dbContext.UrunTur.ToList())
            {
                if (urunTur.Id == id)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = urunTur.Id.ToString(),
                        Text = urunTur.Adi,
                        Selected = true
                    });
                }
                else
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = urunTur.Id.ToString(),
                        Text = urunTur.Adi
                    });
                }
            }
            return selectList;
        }

        public IList<SelectListItem> GetMarkaSelectList(int? id = null)
        {
            IList<SelectListItem> selectList;

            if (id == null)
            {
                selectList = _dbContext.Marka.ToList().
                    Select(t => new SelectListItem() { Text = t.Adi, Value = t.Id.ToString() }).ToList();

                return selectList;
            }

            selectList = new List<SelectListItem>();

            foreach (var marka in _dbContext.Marka.ToList())
            {
                if (marka.Id == id)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = marka.Id.ToString(),
                        Text = marka.Adi,
                        Selected = true
                    });
                }
                else
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = marka.Id.ToString(),
                        Text = marka.Adi
                    });
                }
            }
            return selectList;
        }

      
        public IList<SelectListItem> GetUrunSaglayiciSelectList(int? id = null)
        {
            IList<SelectListItem> selectList;

            if (id == null)
            {
                selectList = _dbContext.UrunSaglayici.ToList().
                    Select(p => new SelectListItem() { Text = p.Adi, Value = p.Id.ToString() }).
                    ToList();

                return selectList;
            }

            selectList = new List<SelectListItem>();

            foreach (var urunsaglayici in _dbContext.UrunSaglayici.ToList())
            {
                if (urunsaglayici.Id == id)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = urunsaglayici.Id.ToString(),
                        Text = urunsaglayici.Adi,
                        Selected = true
                    });
                }
                else
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = urunsaglayici.Id.ToString(),
                        Text = urunsaglayici.Adi
                    });
                }
            }

            return selectList;
        }


        public IList<SelectListItem> GetIlcelerOfSelectedSehir(int? id)
        {
            List<SelectListItem> ılceler = new List<SelectListItem>();

            foreach (var ilce in _dbContext.Ilce.ToList())
            {
                if (ilce.Id == id)
                {
                    ılceler.Add(new SelectListItem
                    {
                        Value = ilce.Id.ToString(),
                        Text = ilce.Adi,
                        Selected = true
                    });
                }
                else
                {
                    ılceler.Add(new SelectListItem
                    {
                        Value = ilce.Id.ToString(),
                        Text = ilce.Adi
                    });
                }
            }
            return ılceler;
        }



        public IList<SelectListItem> GetDepartmanSelectList(int? id = null)
        {
            if (id == null)
            {
                IList<SelectListItem> selectList = _dbContext.Departman.ToList().
                    Select(t => new SelectListItem() { Text = t.Adi, Value = t.Id.ToString() }).
                    ToList();

                return selectList;
            }

            IList<SelectListItem> list = new List<SelectListItem>();

            foreach (var departman in _dbContext.Departman.ToList())
            {
                if (departman.Id == id)
                {
                    list.Add(new SelectListItem
                    {
                        Value = departman.Id.ToString(),
                        Text = departman.Adi,
                        Selected = true
                    });
                }
                else
                {
                    list.Add(new SelectListItem
                    {
                        Value = departman.Id.ToString(),
                        Text = departman.Adi
                    });
                }
            }
            return list;
        }

        public IList<SelectListItem> GetUnvanSelectList(int? id = null)
        {
            if (id == null)
            {
                IList<SelectListItem> selectList = _dbContext.Unvan.ToList().
                    Select(t => new SelectListItem() { Text = t.Adi, Value = t.Id.ToString() }).
                    ToList();

                return selectList;
            }

            IList<SelectListItem> list = new List<SelectListItem>();

            foreach (var unvan in _dbContext.Unvan.ToList())
            {
                if (unvan.Id == id)
                {
                    list.Add(new SelectListItem
                    {
                        Value = unvan.Id.ToString(),
                        Text = unvan.Adi,
                        Selected = true
                    });
                }
                else
                {
                    list.Add(new SelectListItem
                    {
                        Value = unvan.Id.ToString(),
                        Text = unvan.Adi
                    });
                }
            }
            return list;
        }

    }
}

