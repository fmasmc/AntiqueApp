using AntiqueApp.Models;
using AntiqueApp.Repositories;
using AntiqueApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Material = AntiqueApp.Models.Material;

namespace AntiqueApp.ViewModels
{
    public partial class ExhibitManagementViewModel : ObservableObject
    {
        private readonly DataRepository _dataRepository = new DataRepository(); 
        private readonly ExhibitsRepository _exhibitsRepository = new ExhibitsRepository();
        //Автор
        [ObservableProperty]
        private string _authorName = string.Empty;
        [ObservableProperty]
        private string _authorMiddleName = string.Empty;
        [ObservableProperty]
        private string _authorLastName = string.Empty;
        [ObservableProperty]
        private DateTime _authorBirthDate;

        //Экспонат
        [ObservableProperty]
        private Material _material = null!;
        [ObservableProperty]
        private Subcategory _subcategory = null!;
        [ObservableProperty]
        private DateTime _creationYear;
        [ObservableProperty]
        private string _price = string.Empty;
        [ObservableProperty]
        private string _description = string.Empty;
        [ObservableProperty]
        private BitmapImage _picture = null!;

        //Редактирование цены
        [ObservableProperty]
        private Exhibit _selectedExhibit = null!;
        [ObservableProperty]
        private string _editablePrice = string.Empty;

        //Данные в ComBobox
        [ObservableProperty]
        private List<Material> _materials = null!;
        [ObservableProperty]
        private List<Subcategory> _subcategories = null!;
        [ObservableProperty]
        private List<Exhibit> _exhibits = null!;    
        public ExhibitManagementViewModel()
        {
            _materials = _dataRepository.GetMaterials();
            _subcategories = _dataRepository.GetSubcategories();
            _exhibits = _exhibitsRepository.GetExhibits();
        }

        [RelayCommand]
        public void PickPicture()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Выберите изображение",
                Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.webp|Все файлы|*.*"
            };

            if (dialog.ShowDialog() != true)
                return;

            try
            {
                BitmapImage image = new BitmapImage();

                image.BeginInit();
                image.UriSource = new Uri(dialog.FileName);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();

                Picture = image;
            }
            catch
            {
                new MessageService().OpenMessage(
                    "Ошибка",
                    "Выбранный файл не является корректным изображением.");
            }
        }

        [RelayCommand]
        public void CreateExhibit()
        {
            if (string.IsNullOrWhiteSpace(AuthorName) ||
                string.IsNullOrWhiteSpace(AuthorLastName) ||
                string.IsNullOrWhiteSpace(Description) ||
                Material == null ||
                Subcategory == null ||
                Picture == null)
            {
                new MessageService().OpenMessage(
                    "Ошибка",
                    "Заполните все поля.");
                return;
            }

            if (!decimal.TryParse(Price, out decimal price))
            {
                new MessageService().OpenMessage(
                    "Ошибка",
                    "Некорректная цена.");
                return;
            }

            try
            {
                int authorId = _dataRepository.AddAuthor(
                    AuthorName,
                    AuthorMiddleName,
                    AuthorLastName,
                    AuthorBirthDate);

                _exhibitsRepository.AddExhibit(
                    GetImageBytes(),
                    Description,
                    Material.MaterialId,
                    Subcategory.SubcategoryId,
                    price,
                    authorId);

                ClearFields();
            }
            catch (Exception ex)
            {
                new MessageService().OpenMessage(
                    "Ошибка",
                    ex.Message);
            }


        }

        [RelayCommand]
        public void EditPrice()
        {
            if (SelectedExhibit == null)
            {
                new MessageService().OpenMessage(
                    "Ошибка",
                    "Выберите экспонат.");
                return;
            }

            if (string.IsNullOrWhiteSpace(EditablePrice))
            {
                new MessageService().OpenMessage(
                    "Ошибка",
                    "Введите цену.");
                return;
            }

            if (!decimal.TryParse(EditablePrice, out decimal newPrice))
            {
                new MessageService().OpenMessage(
                    "Ошибка",
                    "Цена должна быть числом.");
                return;
            }

            if (newPrice <= 0)
            {
                new MessageService().OpenMessage(
                    "Ошибка",
                    "Цена должна быть больше нуля.");
                return;
            }

            try
            {
                _exhibitsRepository.ChangePrice(
                    SelectedExhibit.ExhibitId,
                    newPrice);

                SelectedExhibit.Price = newPrice;

                new MessageService().OpenMessage(
                    "Успешно",
                    "Цена изменена.");
            }
            catch (Exception ex)
            {
                new MessageService().OpenMessage(
                    "Ошибка",
                    ex.Message);
            }
        }

        private void ClearFields()
        {
            AuthorName = string.Empty;
            AuthorMiddleName = string.Empty;
            AuthorLastName = string.Empty;
            Description = string.Empty;
            Price = string.Empty;
            Picture = null!;
        }

        private byte[] GetImageBytes()
        {
            if (Picture == null)
                return Array.Empty<byte>();

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(Picture));

            using MemoryStream stream = new MemoryStream();
            encoder.Save(stream);

            return stream.ToArray();
        }

        partial void OnSelectedExhibitChanged(Exhibit value)
        {
            EditablePrice = value?.Price.ToString() ?? string.Empty;
        }
    }
}
