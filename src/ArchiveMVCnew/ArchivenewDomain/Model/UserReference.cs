using System;
using System.Collections.Generic;

namespace ArchivenewDomain.Model;

public partial class UserReference: Entity
{
    public int UserrefId { get; set; }

    public int UserId { get; set; }

    public int ReferenceId { get; set; }

    public virtual Reference Reference { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
