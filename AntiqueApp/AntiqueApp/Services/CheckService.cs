using AntiqueApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AntiqueApp.Services
{
    public class CheckService
    {
        public void PrintCheck(Order order)
        {
            string checkHeader =
                $"\nЗаказ №{order.OrderId} от {order.OrderDate}" +
                $"\n===========================================" +
                $"\n" +
                $"\nКлиент: {order.User.FullName}" +
                $"\n===========================================" +
                $"\n" +
                $"\nТовары:" +
                $"\nНаименование                            Цена" +
                $"\n";

            string orderItems = "";

            foreach (var item in order.OrderItems)
            {
                var description = item.Exhibit.Description ?? string.Empty;
                var shortDesc = description.Length > 20 ? description.Substring(0, 20) + "..." : description;

                var result = $"\n{shortDesc,-20}               {item.Price}руб.";

                orderItems += result;
            }

            string footer =
                $"\n" +
                $"\n===========================================" +
                $"\n" +
                $"\nСПОСОБ ОПЛАТЫ: {order.PaymentTypeNavigation.Payment}" +
                $"\nИТОГО: {order.OrderItems.Sum(a => a.Price)}руб.";


            string check = checkHeader + orderItems + footer;

            string filePath = Path.Combine(
                Path.GetTempPath(),
                $"check_{order.OrderId}.txt");

            File.WriteAllText(filePath, check);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "notepad.exe",
                Arguments = $"/p \"{filePath}\"",
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process.Start(psi);
        }
    }
}
