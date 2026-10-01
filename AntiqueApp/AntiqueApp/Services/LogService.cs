using AntiqueApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntiqueApp.Services
{
    public class LogService
    {
        public void LogAuth(int UserId)
        {
            using (var db = new CocojamboContext())
            {
                var attempt = new LoginHistory
                {
                    LoginDate = DateTime.Now,
                    UserId = UserId
                };

                db.LoginHistories.Add(attempt);
                db.SaveChanges();
            }
        }
    }
}
