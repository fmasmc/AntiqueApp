using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Wpf.Ui.Controls;

namespace AntiqueApp.Services
{
    public class MessageService
    {
        public void OpenMessage(string title, string message)
        {
            new Wpf.Ui.Controls.MessageBox { Content = message, Title = title }.ShowDialogAsync();
        }
    }
}
