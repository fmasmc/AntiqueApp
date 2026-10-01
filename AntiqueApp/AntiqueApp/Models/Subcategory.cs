using System;
using System.Collections.Generic;

namespace AntiqueApp.Models;

public partial class Subcategory
{
    public int SubcategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CategorySubcategory> CategorySubcategories { get; set; } = new List<CategorySubcategory>();

    public virtual ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();
}
