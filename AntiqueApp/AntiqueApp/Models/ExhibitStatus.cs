using System;
using System.Collections.Generic;

namespace AntiqueApp.Models;

public partial class ExhibitStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();
}
