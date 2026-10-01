using AntiqueApp.Messages;
using AntiqueApp.Models;
using AntiqueApp.Repositories;
using AntiqueApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AntiqueApp.ViewModels
{
    public partial class OrdersPageViewModel : ObservableObject, IRecipient<OrderItemDeleteMessageAdmin>
    {
        private readonly OrdersRepository _ordersRepository = new();
        [ObservableProperty]
        ObservableCollection<OrderCardEditViewModel> _orders;
        public OrdersPageViewModel()
        {
            var orders = _ordersRepository.GetAllOrders();

            List<OrderCardEditViewModel> orderCards = new();

            foreach (var item in orders)
            {
                orderCards.Add(new OrderCardEditViewModel(item));
            }

            _orders = new ObservableCollection<OrderCardEditViewModel>(orderCards);

            WeakReferenceMessenger.Default.Register<OrderItemDeleteMessageAdmin>(this);
        }

        public void Receive(OrderItemDeleteMessageAdmin message)
        {
            Orders.Remove(message.Value);
        }
    }
}
