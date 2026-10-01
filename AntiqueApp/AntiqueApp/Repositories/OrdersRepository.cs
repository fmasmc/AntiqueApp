using System;
using System.Collections.Generic;
using System.Text;
using AntiqueApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AntiqueApp.Repositories
{
    public class OrdersRepository
    {
        public void AddOrder(List<Exhibit> exhibits, string address, string comment, PaymentType paymentType, User user)
        {
            if (exhibits == null || !exhibits.Any())
                throw new ArgumentNullException(nameof(exhibits), "Выберите хотя бы один экспонат!");

            using (CocojamboContext db = new CocojamboContext())
            {
                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    DeliveryAddress = address,
                    Comment = comment,
                    PaymentType = paymentType.Id,
                    UserId = user.UserId,
                    StatusId = 2,
                    OrderItems = new List<OrderItem>()
                };

                decimal totalOrderPrice = 0;

                foreach (var item in exhibits)
                {
                    var trackedExhibit = db.Exhibits.FirstOrDefault(i => i.ExhibitId == item.ExhibitId);

                    if (trackedExhibit == null)
                        throw new ArgumentException("Выбранный товар не найден");

                    if (trackedExhibit.StatusId == 2)
                        throw new ArgumentException($"Товара {trackedExhibit.Description} нет в наличии");

                    trackedExhibit.StatusId = 2;

                    totalOrderPrice += trackedExhibit.Price;

                    var orderItem = new OrderItem()
                    {
                        Order = order,
                        ExhibitId = trackedExhibit.ExhibitId,
                        Price = trackedExhibit.Price
                    };

                    order.OrderItems.Add(orderItem);
                }

                order.TotalPrice = totalOrderPrice;

                db.Orders.Add(order);

                db.SaveChanges();
            }
        }

        public void EditOrder(List<Exhibit> exhibits, string address, string comment, PaymentType paymentType)
        {
            if (exhibits == null || !exhibits.Any())
                throw new ArgumentNullException(nameof(exhibits), "Выберите хотя бы один экспонат!");

            using (CocojamboContext db = new CocojamboContext())
            {
                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    DeliveryAddress = address,
                    Comment = comment,
                    PaymentType = paymentType.Id
                };

                db.Orders.Update(order);
                db.SaveChanges();

                foreach (Exhibit exhibit in exhibits)
                {
                    var orderItem = new OrderItem()
                    {
                        OrderId = order.OrderId,
                        ExhibitId = exhibit.ExhibitId,
                        Price = exhibit.Price
                    };

                    db.OrderItems.Update(orderItem);
                }
                db.SaveChanges();
                UpdateOrderPrice(order.OrderId);
            }
        }

        public void UpdateOrderPrice(int orderId)
        {
            using (CocojamboContext db = new CocojamboContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order == null) return;

                decimal itemsSubtotal = db.OrderItems
                                          .Where(oi => oi.OrderId == orderId)
                                          .Sum(oi => oi.Price);

                order.TotalPrice = itemsSubtotal;
                db.Orders.Update(order);
                db.SaveChanges();
            }
        }

        public void DeleteOrder(int orderId)
        {
            using (CocojamboContext db = new CocojamboContext())
            {
                var order = db.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Exhibit)
                .FirstOrDefault(o => o.OrderId == orderId);

                if (order == null)
                    return;

                foreach (var item in order.OrderItems)
                {
                    if (item.Exhibit != null)
                    {
                        item.Exhibit.StatusId = 1;
                    }
                }

                db.OrderItems.RemoveRange(order.OrderItems);

                db.Orders.Remove(order);

                db.SaveChanges();
            }
        }

        public List<Order> GetOrdersByUser(int userId)
        {
            using (CocojamboContext db = new CocojamboContext())
            {
                var orders = db.Orders
                    .Where(oi => oi.UserId == userId)
                    .Include(oi => oi.OrderItems)
                        .ThenInclude(a => a.Exhibit)
                    .ToList();
                return orders;
            }
        }

        public Order GetOrderById(int orderId)
        {
            using (CocojamboContext db = new CocojamboContext())
            {
                var order = db.Orders
                    .Where(oi => oi.OrderId == orderId)
                    .Include(oi => oi.User)
                    .Include(oi => oi.PaymentTypeNavigation)
                    .Include(oi => oi.OrderItems)
                        .ThenInclude(a => a.Exhibit)
                    .FirstOrDefault();
                return order;
            }
        }

        public List<Order> GetAllOrders()
        {
            using (CocojamboContext db = new CocojamboContext())
            {
                var orders = db.Orders
                    .Include(oi => oi.User)
                    .Include(oi => oi.PaymentTypeNavigation)
                    .Include(oi => oi.OrderItems)
                        .ThenInclude(a => a.Exhibit)
                    .ToList();
                return orders;
            }
        }

        public void ChangeOrderStatus(int orderId, int statusId)
        {
            using (CocojamboContext db = new CocojamboContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order == null) return;
                order.StatusId = statusId;
                db.Orders.Update(order);
                db.SaveChanges();
            }
        }
    }
}
