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
    public partial class CartViewModel : ObservableObject, IRecipient<CartItemDeletedMessage>
    {
        private readonly CartService _cartService = CartService.Instance;
        private readonly MessageService _messageService = new();
        private readonly OrdersRepository _ordersRepository = new();
        private readonly DataRepository _dataRepository = new();
        private readonly CheckService _checkService = new();
        private readonly UserService _userService = UserService.Instance;
        [ObservableProperty]
        private ObservableCollection<CartItemViewModel> _cartItems = new();
        [ObservableProperty]
        private ObservableCollection<PaymentType> _paymentTypes = new();
        [ObservableProperty]
        private string _address = string.Empty;
        [ObservableProperty]
        private string _commentary = string.Empty;
        [ObservableProperty]
        private PaymentType _paymentType = null!;
        [ObservableProperty]
        private string _totalAmount = string.Empty;
        private List<Exhibit> _exhibitsList = new();

        public CartViewModel()
        {
            _paymentTypes = new ObservableCollection<PaymentType>(_dataRepository.GetPaymentTypes());

            _exhibitsList = _cartService.GetExhibitsInCart();

            foreach (Exhibit exhibit in _exhibitsList)
            {
                _cartItems.Add(new CartItemViewModel(exhibit));
            }

            CalculateTotalPrice();

            WeakReferenceMessenger.Default.Register<CartItemDeletedMessage>(this);
        }

        public void Receive(CartItemDeletedMessage message)
        {
            CartItems.Remove(message.Value);
            CalculateTotalPrice();
        }

        [RelayCommand]
        public void CreateOrder()
        {
            if (CartItems.Count == 0)
            {
                _messageService.OpenMessage("Ошибка", "Корзина пуста");
                return;
            }

            if (Address == string.Empty)
            {
                _messageService.OpenMessage("Ошибка", "Введите адрес доставки");
                return;
            }

            List<Exhibit> cartItems = new();

            foreach (var item in CartItems)
            {
                cartItems.Add(item.GetCardExhibitObject());
            }

            try
            {
                _ordersRepository.AddOrder(cartItems, Address, Commentary, PaymentType, _userService.GetCurentUser());
                _messageService.OpenMessage("Успех", "Заказ добавлен");
            }
            catch (Exception ex)
            {
                _messageService.OpenMessage("Ошибка", ex.Message);
            }
        }

        private void CalculateTotalPrice()
        {
            TotalAmount = _exhibitsList.Sum(a => a.Price).ToString();
        }
    }
}
