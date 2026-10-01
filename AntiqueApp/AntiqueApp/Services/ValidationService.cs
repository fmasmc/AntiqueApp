using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AntiqueApp.Services
{
    public class ValidationService
    {
        public void ValidateNotNull(string str)
        {
            if (str.IsNullOrEmpty())
                throw new ArgumentException("Поля не могут быть пустыми");
        }

        public void ValidatePassword(string password)
        {
            var regex = new Regex("^(?=.*[A-Za-zА-Яа-я])(?=.*\\d).{8,}$");
            if (!regex.IsMatch(password))
                throw new ArgumentException("Пароль должен быть от 8 символов и содержать минимум одну букву и цифру.");
        }

        public void ValidateMail(string mail)
        {
            var regex = new Regex(@"^[a-zA-Z0-9_-]+@[a-zA-Z]+\.[a-z]{2,}$");
            if (!regex.IsMatch(mail))
                throw new ArgumentException("Такой почты не может существовать");
        }

        public void ValidatePhone(string phone)
        {
            var regex = new Regex("^(?:\\+7|8)?[\\s\\-]?\\(?\\d{3}\\)?[\\s\\-]?\\d{3}[\\s\\-]?\\d{2}[\\s\\-]?\\d{2}$");
            if (!regex.IsMatch(phone))
                throw new ArgumentException("Номер телефона неверного формата");
        }


    }
}
