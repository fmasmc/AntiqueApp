using AntiqueApp.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntiqueApp.Messages
{
    public class OrderItemDeleteMessage : ValueChangedMessage<OrderCartViewModel>
    {
        public OrderItemDeleteMessage(OrderCartViewModel value) : base(value)
        {
        }
    }
}
