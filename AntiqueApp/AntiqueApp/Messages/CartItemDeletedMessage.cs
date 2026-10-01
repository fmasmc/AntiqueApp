using AntiqueApp.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntiqueApp.Messages
{
    public class CartItemDeletedMessage : ValueChangedMessage<CartItemViewModel>
    {
        public CartItemDeletedMessage(CartItemViewModel value) : base(value)
        {
        }
    }
}
