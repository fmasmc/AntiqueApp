using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using AntiqueApp.Models;
using AntiqueApp.Repositories;
using AntiqueApp.Services;
using AntiqueApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Controls;


namespace AntiqueApp.ViewModels
{
    public partial class AuthViewModel : ObservableObject
    {
        private PermissionService _permissionService = PermissionService.Instance;
        private UserService _userService = UserService.Instance;
        private readonly MessageService _messageService = new();
        private readonly UsersRepository _usersRepository = new();
        private readonly LogService _logService = new();
        [ObservableProperty]
        private string _password = string.Empty;
        [ObservableProperty]
        private string _login = string.Empty;

        [RelayCommand]
        public void Auth()
        {
            try
            {
                if (_usersRepository.Auth(Login, Password))
                {
                    var user = _usersRepository.GetUserByLogin(Login);

                    if (user.Role.Name == "Администратор")
                    {
                        _permissionService.SetAdminPermissions();
                    }
                    if (user.Role.Name == "Пользователь")
                    {
                        _permissionService.SetUserPermissions();
                    }

                    _userService.ChangeUser(user);
                    _logService.LogAuth(user.UserId);

                    var userView = new UserView();
                    userView.Show();
                    CloseCurrentWindow();
                }
            }
            catch(ArgumentException ex)
            {
                _messageService.OpenMessage("Ошибка", $"{ex.Message}");
            }
        }

        [RelayCommand]
        public void ContinueWithoutAuth()
        {
            var userView = new UserView();
            userView.Show();
            CloseCurrentWindow();
        }
        [RelayCommand]
        public void Registration()
        {
            RegView regView = new RegView();
            regView.Show();
            CloseCurrentWindow();
        }

        public void UpdatePassword(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                Password = passwordBox.Password;
            }
        }

        private void CloseCurrentWindow()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window is AuthView)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
