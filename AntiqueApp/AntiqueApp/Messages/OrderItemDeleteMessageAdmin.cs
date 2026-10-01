using AntiqueApp.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntiqueApp.Messages
{
    public class OrderItemDeleteMessageAdmin : ValueChangedMessage<OrderCardEditViewModel>
    {
        public OrderItemDeleteMessageAdmin(OrderCardEditViewModel value) : base(value)
        {
        }
    }
}
