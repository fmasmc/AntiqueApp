using System;
using System.Collections.Generic;

namespace AntiqueApp.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CategorySubcategory> CategorySubcategories { get; set; } = new List<CategorySubcategory>();
}
