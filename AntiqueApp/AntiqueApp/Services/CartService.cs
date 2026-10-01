using AntiqueApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntiqueApp.Services
{
    public class CartService
    {
        public static CartService Instance { get; } = new();
        private CartService() { }

        private List<Exhibit> exhibitsInCart = new();

        public void AddToCart(Exhibit exhibit)
        {
            if (!exhibitsInCart.Contains(exhibit))
            {
                exhibitsInCart.Add(exhibit);
            }
            else
            {
                throw new ArgumentException("Этот товар уже добавлен в корзину");
            }
        }

        public void RemoveFromCart(Exhibit exhibit)
        {
            if (exhibitsInCart.Contains(exhibit))
            {
                exhibitsInCart.Remove(exhibit);
            }
            else
            {
                throw new ArgumentException("Этот товар не найден в корзине");
            }
        }

        public List<Exhibit> GetExhibitsInCart()
        {
            return exhibitsInCart;
        }
    }
}
