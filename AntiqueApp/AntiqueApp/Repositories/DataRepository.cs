using AntiqueApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntiqueApp.Repositories
{
    internal class DataRepository
    {
        public List<PaymentType> GetPaymentTypes()
        {
            using (var db = new CocojamboContext())
            {
                var paymentTypes = db.PaymentTypes.ToList();
                return paymentTypes;
            }
        }

        public List<Material> GetMaterials()
        {
            using (var db = new CocojamboContext())
            {
                var materials = db.Materials.ToList();
                return materials;
            }
        }

        public List<Subcategory> GetSubcategories()
        {
            using (var db = new CocojamboContext())
            {
                var subcat = db.Subcategories.ToList();
                return subcat;
            }
        }

        public int AddAuthor(string firstName, string middleName, string lastName, DateTime birthDate)
        {
            using (var db = new CocojamboContext())
            {
                var author = new Author
                {
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    BirthDate = DateOnly.FromDateTime(birthDate)
                };

                db.Authors.Add(author);
                db.SaveChanges();

                return author.AuthorId;
            }
        }

        public List<OrderStatus> GetOrderStatuses()
        {
            using (var db = new CocojamboContext())
            {
                var orderStatuses = db.OrderStatuses.ToList();
                return orderStatuses;
            }
        }

        public List<User> GetAllUsers()
        {
            using (var db = new CocojamboContext())
            {
                var users = db.Users
                    .Include(o => o.Orders)
                        .ThenInclude(o => o.OrderItems)
                    .ToList();
                return users;
            }
        }

        public List<LoginHistory> GetLoginHistory()
        {
            using (var db = new CocojamboContext())
            {
                return db.LoginHistories.Include(o => o.User).ToList();
            }
        }
    }
}
