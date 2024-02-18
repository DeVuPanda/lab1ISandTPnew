using System;
using System.Collections.Generic;

namespace ArchivenewDomain.Model;

public partial class Date: Entity
{
    public int DateId { get; set; }

    public string FullName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Faculty { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string Format { get; set; } = null!;

    public string ExtentOfMaterial { get; set; } = null!;

    public DateOnly Date1 { get; set; }

    public virtual ICollection<DateReference> DateReferences { get; set; } = new List<DateReference>();
}
