using System;
using System.Collections.Generic;

namespace ArchivenewDomain.Model;

public partial class SearchObject: Entity
{
    public int SoId { get; set; }

    public int ReferenceId { get; set; }

    public string FullName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Faculty { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly SearchTime { get; set; }

    public virtual Reference Reference { get; set; } = null!;

    public virtual ICollection<SearchHistory> SearchHistories { get; set; } = new List<SearchHistory>();
}
