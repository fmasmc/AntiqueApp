using AntiqueApp.Models;
using AntiqueApp.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace AntiqueApp.ViewModels
{
    public partial class ClienPageViewModel : ObservableObject
    {
        private readonly DataRepository _dataRepository = new();
        [ObservableProperty]
        ObservableCollection<ClientCardViewModel> _clients;

        public ClienPageViewModel()
        {
            var orders = _dataRepository.GetAllUsers();

            List<ClientCardViewModel> orderCards = new();

            foreach (var item in orders)
            {
                orderCards.Add(new ClientCardViewModel(item));
            }

            Clients = new ObservableCollection<ClientCardViewModel>(orderCards);
        }
    }
}
