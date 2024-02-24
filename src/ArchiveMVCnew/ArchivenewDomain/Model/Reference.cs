using System;
using System.Collections.Generic;

namespace ArchivenewDomain.Model;

public partial class Reference
{
    public int ReferenceId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string Searchable { get; set; } = null!;

    public int SoId { get; set; }

    public virtual ICollection<DateReference> DateReferences { get; set; } = new List<DateReference>();

    public virtual ICollection<SearchObject> SearchObjects { get; set; } = new List<SearchObject>();

    public virtual ICollection<UserReference> UserReferences { get; set; } = new List<UserReference>();
}
