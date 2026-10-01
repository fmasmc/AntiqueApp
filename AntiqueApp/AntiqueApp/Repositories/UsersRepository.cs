using AntiqueApp.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AntiqueApp.Repositories
{
    public class UsersRepository
    {
        public void CreateUser(
            string lastName,
            string firstName,
            string middleName,
            string phone, 
            string address, 
            string email, 
            string login, 
            string password)
        {
            using (var db = new CocojamboContext())
            {
                if (db.Users.FirstOrDefault(u => u.Login == login) != null)
                    throw new ArgumentException("Пользователь с таким логином уже существует");

                if (db.Users.FirstOrDefault(u => u.Email == email) != null)
                    throw new ArgumentException("Пользователь с такой почтой уже существует");

                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                    Phone = phone,
                    DeliveryAddress = address,
                    Email = email,
                    Login = login,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password),
                    RoleId = 2
                };

                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void EditUser(
            string firstName,
            string middleName,
            string lastName,
            string email,
            string address,
            string phone,
            string login, // Логин на который заменить
            string password,
            User currentUser)
        {
            using (var db = new CocojamboContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == login);

                if (user != null)
                {
                    if (user.Login != currentUser.Login)
                    {
                        throw new ArgumentException("Пользователь с таким логином уже существует");
                    }
                    if (user.Email != currentUser.Email)
                    {
                        throw new ArgumentException("Пользователь с такой почтой уже существует");
                    }
                }

                var changedUser = db.Users.FirstOrDefault(u => u.UserId == currentUser.UserId);

                if (changedUser == null)
                    throw new ArgumentException("Не удалось изменить пользователя");

                changedUser.FirstName = firstName;
                changedUser.MiddleName = middleName;
                changedUser.LastName = lastName;
                changedUser.Phone = phone;
                changedUser.DeliveryAddress = address;
                changedUser.Email = email;
                changedUser.Login = login;
                changedUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

                db.Users.Update(changedUser);
                db.SaveChanges();
            }
        }

        public bool Auth(string login, string password)
        {
            using (var db = new CocojamboContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == login);

                if (user == null)
                    throw new ArgumentException("Пользователь не найден");

                if (BCrypt.Net.BCrypt.EnhancedVerify(password ,user.Password))
                    return true;

                throw new ArgumentException("Неверное имя пользователя или пароль");
            }
        }
        public User GetUserByLogin(string login)
        {
            using (var db = new CocojamboContext())
            {
                var user = db.Users.Include(u => u.Role)
                    .FirstOrDefault(u => u.Login == login);
                
                if (user == null)
                    throw new ArgumentException("Пользователь не найден");

                return user;
            }
        }

    }
}
