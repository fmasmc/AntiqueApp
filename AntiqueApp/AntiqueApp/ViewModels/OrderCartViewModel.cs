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
using System.Windows;

namespace AntiqueApp.ViewModels
{
    public partial class OrderCartViewModel : ObservableObject
    {
        private readonly OrdersRepository _orderRepository = new();
        private readonly MessageService _messageService = new MessageService();
        private readonly CheckService _checkService = new CheckService();
        [ObservableProperty]
        private string _id = string.Empty;
        [ObservableProperty]
        private string _orderItems = string.Empty;
        [ObservableProperty]
        private Visibility _buttonCancelOrderVisibility = Visibility.Collapsed;
        private readonly Order _order;
        public OrderCartViewModel(Order order)
        {
            _order = order;
            Id = $"Заказ номер {order.OrderId.ToString()} от {order.OrderDate}";
            foreach (var item in order.OrderItems)
            {
                OrderItems += $"{item.Exhibit.Description}" + "\n";
            }

            if (order.StatusId != 3 && order.StatusId !=4)
                _buttonCancelOrderVisibility = Visibility.Visible;
        }
        [RelayCommand]
        public void CancelOrder()
        {
            _orderRepository.DeleteOrder(_order.OrderId);
            _messageService.OpenMessage("Успех", "Заказ отменен");

            WeakReferenceMessenger.Default.Send(
                new OrderItemDeleteMessage(this));
        }

        [RelayCommand]
        public void PrintCheck()
        {
            var order = _orderRepository.GetOrderById(_order.OrderId);
            _checkService.PrintCheck(order);
        }
    }
}
