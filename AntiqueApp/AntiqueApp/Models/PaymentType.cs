using System;
using System.Collections.Generic;

namespace AntiqueApp.Models;

public partial class PaymentType
{
    public int Id { get; set; }

    public string? Payment { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
