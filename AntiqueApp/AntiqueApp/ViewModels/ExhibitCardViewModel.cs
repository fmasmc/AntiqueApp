using AntiqueApp.Models;
using AntiqueApp.Services;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AntiqueApp.ViewModels
{
    public partial class ExhibitCardViewModel : ObservableObject
    {
        private readonly CartService _cartService = CartService.Instance;
        private readonly PermissionService _permissionService = PermissionService.Instance;
        private readonly MessageService _messageService = new();
        private readonly ImageService _imageService = new();
        private readonly Exhibit _exhibit;
        [ObservableProperty]
        private BitmapImage _image = null!;
        [ObservableProperty]
        private string _author = string.Empty;
        [ObservableProperty]
        private string _description = string.Empty;
        [ObservableProperty]
        private string _material = string.Empty;
        [ObservableProperty]
        private string _subcategory = string.Empty;
        [ObservableProperty]
        private string _price = string.Empty;
        [ObservableProperty]
        private string _year = string.Empty;

        public Visibility AddToCartVisibility =>
        PermissionService.Instance.HasPermission("exhibits.add_to_cart")
            ? Visibility.Visible
            : Visibility.Collapsed;

        public ExhibitCardViewModel(Exhibit exhibit)
        {
            if (exhibit == null)
                return;

            _image = _imageService.ConvertBytesToBitmapImage(exhibit.Photo);
            _author = exhibit.Author.FullName;
            _description = exhibit.Description;
            _material = exhibit.Material.Name;
            _subcategory = exhibit.Subcategory.Name;
            _price = $"{exhibit.Price} руб.";
            _year = exhibit.YearCreated.ToString();
            _exhibit = exhibit;
        }

        [RelayCommand]
        public void AddToCart()
        {
            try
            {
                _cartService.AddToCart(_exhibit);
            }
            catch (Exception ex)
            {
                _messageService.OpenMessage("Ошибка", ex.Message);
            }
        }
    }
}
