using AntiqueApp.Models;
using AntiqueApp.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AntiqueApp.Repositories
{
    public class ExhibitsRepository
    {
        private readonly MessageService _messageService = new MessageService();
        public List<Exhibit> GetExhibitsViaCategory(string categoryName)
        {
            using (var db = new CocojamboContext())
            {
                var exh = db.Exhibits
                    .Where(e => e.Subcategory.Name == categoryName)
                    .Where(e => e.StatusId == 1)
                    .Include(s => s.Subcategory)
                    .Include(s => s.Author)
                    .Include(s => s.Material)
                    .ToList();

                return exh;
            }
        }
        public void AddExhibit(byte[] photo, string description, int material, int subcat, decimal price, int author)
        {
            using (CocojamboContext db = new CocojamboContext())
            {
                var exhibit = new Exhibit
                {
                    Description = description,
                    Price = price,
                    SubcategoryId = subcat,
                    StatusId = 1,     // По умолчанию
                    AuthorId = author,
                    MaterialId = material,
                    YearCreated = DateTime.Now.Year,
                    Photo = photo
                };

                db.Exhibits.Add(exhibit);
                db.SaveChanges();
                MessageService messageService = new MessageService();
                messageService.OpenMessage("Успешно!", "Экспонат добален");
            }
        }
        public void SellExhibit(Exhibit exhibit)
        {
            using (CocojamboContext db = new CocojamboContext())
            {
                exhibit.ExhibitId = 2;
                db.Exhibits.Update(exhibit);
                db.SaveChanges();

                _messageService.OpenMessage("Успешно!", "Экспонат добален");
            }
        }

        public List<Exhibit> GetExhibits()
        {
            using (CocojamboContext db = new CocojamboContext())
            {
                var exh = db.Exhibits
                    .Where(e => e.StatusId == 1)
                    .ToList();

                return exh;
            }
        }

        public void ChangePrice(int exId, decimal price)
        {
            using (CocojamboContext db = new CocojamboContext())
            {
                var exhibit = db.Exhibits.FirstOrDefault(e => e.ExhibitId == exId);

                if (exhibit != null)
                {
                    exhibit.Price = price;
                    db.SaveChanges();
                }
            }
        }
    }
}
