using System;
using System.Collections.Generic;

namespace ArchivenewDomain.Model;

public partial class SearchHistory
{ 
    public int UserId { get; set; }

    public int SearchSuccess { get; set; }

    public DateOnly SearchDate { get; set; }

    public int SoId { get; set; }

    public virtual SearchObject So { get; set; } = null!;

    public virtual User? User { get; set; }
}
