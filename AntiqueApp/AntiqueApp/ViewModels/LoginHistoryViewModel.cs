using AntiqueApp.Models;
using AntiqueApp.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AntiqueApp.ViewModels
{
    public partial class LoginHistoryViewModel : ObservableObject
    {
        private readonly DataRepository _repository = new();
        [ObservableProperty]
        ObservableCollection<LoginHistory> _loginHistory;

        public LoginHistoryViewModel()
        {
            _loginHistory = new ObservableCollection<LoginHistory>(_repository.GetLoginHistory());
        }
    }
}
