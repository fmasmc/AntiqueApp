using AntiqueApp.Messages;
using AntiqueApp.Models;
using AntiqueApp.Repositories;
using AntiqueApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AntiqueApp.ViewModels
{
    public partial class ProfileViewModel : ObservableObject, IRecipient<OrderItemDeleteMessage>
    {
        [ObservableProperty]
        private ObservableCollection<OrderCartViewModel> _orders;
        private readonly UsersRepository _usersRepository = new UsersRepository();
        private readonly OrdersRepository _ordersRepository = new();
        private readonly UserService _userService = UserService.Instance;
        private readonly MessageService _messageService = new();
        private readonly ValidationService _validationService = new();
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
        [ObservableProperty]
        private string _confirmPassword = string.Empty;
        private void RefreshUserData()
        {
            try
            {
                var currentUser = _userService.GetCurentUser();

                if (currentUser == null)
                {
                    _messageService.OpenMessage("Ошибка" , "Учетная запись не найдена!");
                    return;
                }

                FirstName = currentUser.FirstName;
                MiddleName = currentUser.MiddleName;
                LastName = currentUser.LastName;
                Phone = currentUser.Phone;
                DeliveryAddress = currentUser.DeliveryAddress;
                Email = currentUser.Email;
                Login = currentUser.Login;
            }
            catch (Exception ex)
            {
                _messageService.OpenMessage("Ошибка", $"Ошибка загрузки данных: {ex.Message}");
            }
        }
        [RelayCommand]
        private void ChangeUserData()
        {
            try
            {
                _validationService.ValidatePassword(Password);
                _validationService.ValidateNotNull(FirstName);
                _validationService.ValidateNotNull(MiddleName);
                _validationService.ValidateNotNull(LastName);
                _validationService.ValidateNotNull(Email);
                _validationService.ValidateMail(Email);
                _validationService.ValidateNotNull(DeliveryAddress);
                _validationService.ValidateNotNull(Phone);
                _validationService.ValidatePhone(Phone);
                _validationService.ValidateNotNull(Login);
                _usersRepository.EditUser(FirstName, MiddleName, LastName, Email, DeliveryAddress, Phone, Login, Password, _userService.GetCurentUser());

                _messageService.OpenMessage("Успех", "Данные успешно изменены");
            }
            catch (Exception ex)
            {
                _messageService.OpenMessage("Ошибка", $"{ex.Message}");
                return;
            }
        }
        public ProfileViewModel()
        {
            var orders = _ordersRepository.GetOrdersByUser(_userService.GetCurentUser().UserId);

            List<OrderCartViewModel> orderCards = new();

            foreach (var item in orders)
            {
                orderCards.Add(new OrderCartViewModel(item));
            }

            _orders = new ObservableCollection<OrderCartViewModel>(orderCards);
            RefreshUserData();

            WeakReferenceMessenger.Default.Register<OrderItemDeleteMessage>(this);
        }

        public void Receive(OrderItemDeleteMessage message)
        {
            Orders.Remove(message.Value);
        }
    }
}
