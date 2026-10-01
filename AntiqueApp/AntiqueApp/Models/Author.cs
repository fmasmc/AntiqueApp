using System;
using System.Collections.Generic;

namespace AntiqueApp.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public DateOnly? BirthDate { get; set; }

    public virtual ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();

    public string FullName { get { return $"{MiddleName} {FirstName[0]}. {LastName[0]}."; } }
}
