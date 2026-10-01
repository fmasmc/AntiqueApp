using AntiqueApp.Models;
using AntiqueApp.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AntiqueApp.ViewModels
{
    public partial class ExhibitPageViewModel : ObservableObject
    {
        private readonly ExhibitsRepository _exhibitsRepository = new();
        [ObservableProperty]
        private ObservableCollection<ExhibitCardViewModel> _exhibits;

        public ExhibitPageViewModel(string category)
        {
            var exhibits = _exhibitsRepository.GetExhibitsViaCategory(category);

            List<ExhibitCardViewModel> exhibitsCards = new();

            foreach (var item in exhibits)
            {
                exhibitsCards.Add(new ExhibitCardViewModel(item));
            }

            _exhibits = new ObservableCollection<ExhibitCardViewModel>(exhibitsCards);
        }
    }
}
