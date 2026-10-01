using AntiqueApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntiqueApp.Services
{
    public class UserService
    {
        public static UserService Instance { get; } = new();

        private UserService() { }

        private User _user = new();

        public void ChangeUser(User user)
        {
            _user = user;
        }

        public User GetCurentUser()
        {
            return _user;
        }
    }
}
