using System;
using System.Collections.Generic;

namespace AntiqueApp.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int ExhibitId { get; set; }

    public decimal Price { get; set; }

    public virtual Exhibit Exhibit { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
