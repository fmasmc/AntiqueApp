using AntiqueApp.Models;
using AntiqueApp.Repositories;
using AntiqueApp.Services;
using AntiqueApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace AntiqueApp.ViewModels
{
    public partial class RegViewModel : ObservableValidator 
    {
        private readonly ValidationService _validationService = new();
        private readonly MessageService _messageService = new();
        private readonly UsersRepository _usersRepository = new();
        [ObservableProperty]
        private string _firstName = string.Empty;
        [ObservableProperty]
        private string _middleName = string.Empty;
        [ObservableProperty]
        private string _lastName = string.Empty;
        [ObservableProperty]
        private string _phone = string.Empty;
        [ObservableProperty]
        private string _deliveryAddress = string.Empty;
        [ObservableProperty]
        private string _email = string.Empty;
        [ObservableProperty]
        private string _login = string.Empty;
        [ObservableProperty]
        private string _password = string.Empty;

        [RelayCommand]
        public void Register()
        {
            try
            {
                _validationService.ValidateNotNull(FirstName);
                _validationService.ValidateNotNull(MiddleName);
                _validationService.ValidateNotNull(LastName);
                _validationService.ValidateNotNull(Phone);
                _validationService.ValidatePhone(Phone);
                _validationService.ValidateNotNull(DeliveryAddress);
                _validationService.ValidateNotNull(Email);
                _validationService.ValidateMail(Email);
                _validationService.ValidateNotNull(Login);
                _validationService.ValidateNotNull(Password);
                _validationService.ValidatePassword(Password);

                _usersRepository.CreateUser(
                    LastName,
                    FirstName,
                    MiddleName,
                    Phone,
                    DeliveryAddress,
                    Email,
                    Login,
                    Password);

                _messageService.OpenMessage("Сообщение", "Вы успешно зарегистрировались");

                var view = new AuthView();
                view.Show();

                CloseCurrentWindow();
            }
            catch (Exception ex)
            {
                _messageService.OpenMessage("Ошибка",$"{ex.Message}");
            }
        }

        private void CloseCurrentWindow()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window is RegView)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
