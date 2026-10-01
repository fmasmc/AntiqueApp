using System;
using System.Collections.Generic;

namespace AntiqueApp.Models;

public partial class Exhibit
{
    public int ExhibitId { get; set; }

    public int AuthorId { get; set; }

    public int MaterialId { get; set; } //

    public int SubcategoryId { get; set; } //

    public int StatusId { get; set; } 

    public int? YearCreated { get; set; } //

    public string? Description { get; set; } //

    public decimal Price { get; set; }

    public byte[]? Photo { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Material Material { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ExhibitStatus Status { get; set; } = null!;

    public virtual Subcategory Subcategory { get; set; } = null!;
}
