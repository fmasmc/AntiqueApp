using AntiqueApp.Models;
using AntiqueApp.Services;
using AntiqueApp.Views.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AntiqueApp.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        [ObservableProperty]
        private Page _rootPage = null!;

        public Visibility ManageExhibitsVisibility =>
        PermissionService.Instance.HasPermission("exhibits.manage")
            ? Visibility.Visible
            : Visibility.Collapsed;

        public Visibility OrdersVisibility =>
            PermissionService.Instance.HasPermission("orders.manage")
                ? Visibility.Visible
                : Visibility.Collapsed;

        public Visibility ClientsVisibility =>
            PermissionService.Instance.HasPermission("clients.manage")
                ? Visibility.Visible
                : Visibility.Collapsed;

        public Visibility LoginHistoryVisibility =>
            PermissionService.Instance.HasPermission("loginhistory.view")
                ? Visibility.Visible
                : Visibility.Collapsed;

        public Visibility OpenCartVisibility =>
            PermissionService.Instance.HasPermission("cart.view")
                ? Visibility.Visible
                : Visibility.Collapsed;

        public Visibility ProfileVisibility =>
            PermissionService.Instance.HasPermission("profile.view")
                ? Visibility.Visible
                : Visibility.Collapsed;


        [RelayCommand]
        public void UpdatePage(string category)
        {
            RootPage = new ExibitPage(category);
        }

        [RelayCommand]
        public void OpenProfilePage()
        {
            RootPage = new ProfilePage();
        }

        [RelayCommand]
        public void OpenCartPage()
        {
            RootPage = new CartPage();
        }

        [RelayCommand]
        public void OpenManageExhibitsPage()
        {
            RootPage = new ExhibitManagementPage();
        }

        [RelayCommand]
        public void OpenOrderPages()
        {
            RootPage = new OrdersPage();
        }

        
        [RelayCommand]
        public void OpenClientsPages()
        {
            RootPage = new ClientsPage();
        }
        
        [RelayCommand]
        public void OpenLoginHistory()
        {
            RootPage = new Views.Pages.LoginHistory();
        }
    }
}
