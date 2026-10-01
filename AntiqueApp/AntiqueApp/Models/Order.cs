using System;
using System.Collections.Generic;

namespace AntiqueApp.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public int PaymentType { get; set; }

    public decimal TotalPrice { get; set; }

    public string? Comment { get; set; }

    public DateTime OrderDate { get; set; }

    public int StatusId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual PaymentType PaymentTypeNavigation { get; set; } = null!;

    public virtual OrderStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
