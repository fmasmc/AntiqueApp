using AntiqueApp.Messages;
using AntiqueApp.Models;
using AntiqueApp.Repositories;
using AntiqueApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AntiqueApp.ViewModels
{
    public partial class OrderCardEditViewModel : ObservableObject
    {
        private readonly OrdersRepository _orderRepository = new();
        private readonly DataRepository _dataRepository = new();
        private readonly MessageService _messageService = new();
        private readonly Order _order;
        [ObservableProperty]
        private string _id = string.Empty;
        [ObservableProperty]
        private string _orderItems = string.Empty;

        [ObservableProperty]
        private OrderStatus _selectedStatus = null!;
        [ObservableProperty]
        private List<OrderStatus> _statuses = null!;

        private bool _isInitialized;

        public OrderCardEditViewModel(Order order)
        {
            _order = order;
            Id = $"Заказ номер {order.OrderId.ToString()} от {order.OrderDate} пользователя {order.User.FullName}";
            foreach (var item in order.OrderItems)
            {
                OrderItems += $"{item.Exhibit.Description}" + "\n";
            }

            Statuses = _dataRepository.GetOrderStatuses();
            SelectedStatus = Statuses.FirstOrDefault(s => s.StatusId == order.StatusId);

            _isInitialized = true;

        }

        partial void OnSelectedStatusChanged(OrderStatus value)
        {
            if (!_isInitialized || value == null)
                return;

            _orderRepository.ChangeOrderStatus(_order.OrderId, value.StatusId);
        }

        [RelayCommand]
        public void CancelOrder()
        {
            _orderRepository.DeleteOrder(_order.OrderId);
            _messageService.OpenMessage("Успех", "Заказ отменен");

            WeakReferenceMessenger.Default.Send(
                new OrderItemDeleteMessageAdmin(this));
        }
    }
}
