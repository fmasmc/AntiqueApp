using AntiqueApp.Messages;
using AntiqueApp.Models;
using AntiqueApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AntiqueApp.ViewModels
{
    public partial class CartItemViewModel : ObservableObject
    {
        private readonly CartService _cartService = CartService.Instance;
        private readonly ImageService _imageService= new();
        private Exhibit _exhibit;
        [ObservableProperty]
        private BitmapImage _image;
        [ObservableProperty]
        private string _title = string.Empty;
        [ObservableProperty]
        private string _price = string.Empty;

        public CartItemViewModel(Exhibit exhibit)
        {
            _exhibit = exhibit;
            _image = _imageService.ConvertBytesToBitmapImage(exhibit.Photo);
            _title = exhibit.Description;
            _price = exhibit.Price.ToString();
        }

        [RelayCommand]
        public void DeleteFromCart()
        {
            _cartService.RemoveFromCart(_exhibit);

            WeakReferenceMessenger.Default.Send(
                new CartItemDeletedMessage(this));
        }

        public Exhibit GetCardExhibitObject() => _exhibit;
    }
}
