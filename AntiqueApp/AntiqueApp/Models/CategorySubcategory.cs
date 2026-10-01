using System;
using System.Collections.Generic;

namespace AntiqueApp.Models;

public partial class CategorySubcategory
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int SubcategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Subcategory Subcategory { get; set; } = null!;
}
