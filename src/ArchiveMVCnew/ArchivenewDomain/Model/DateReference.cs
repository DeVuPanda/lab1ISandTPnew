using System;
using System.Collections.Generic;

namespace ArchivenewDomain.Model;

public partial class DateReference: Entity
{
    public int DaterefId { get; set; }

    public int ReferenceId { get; set; }

    public int DateId { get; set; }

    public virtual Date Date { get; set; } = null!;

    public virtual Reference Reference { get; set; } = null!;
}
