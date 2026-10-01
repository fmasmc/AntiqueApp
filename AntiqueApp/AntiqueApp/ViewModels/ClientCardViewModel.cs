using AntiqueApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntiqueApp.ViewModels
{
    public partial class ClientCardViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _login = string.Empty;
        [ObservableProperty]
        private string _registratonData = string.Empty;
        [ObservableProperty]
        private string _ordersCount = string.Empty;
        [ObservableProperty]
        private string _ordersSum = string.Empty;

        public ClientCardViewModel(User user)
        {
            _login = user.Login;

            _registratonData =
                $"\nИмя: {user.FirstName}" +
                $"\nФамилия: {user.MiddleName}" +
                $"\nОтчество: {user.LastName}" +
                $"\nТелефон: {user.Phone}" +
                $"\nЭлкетронная почта: {user.Email}" +
                $"\nАдресс доставки: {user.DeliveryAddress}" +
                $"\n";

            _ordersCount = $"Сделано заказов: {user.Orders.Count}";
            _ordersSum = $"Общая сумма заказов: {user.Orders.Sum(o => o.TotalPrice)}";
        }
    }
}
