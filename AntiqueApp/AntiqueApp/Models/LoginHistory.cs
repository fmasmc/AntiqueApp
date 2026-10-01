using System;
using System.Collections.Generic;

namespace AntiqueApp.Models;

public partial class LoginHistory
{
    public int HistoryId { get; set; }

    public DateTime LoginDate { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
