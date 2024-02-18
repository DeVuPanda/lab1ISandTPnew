using System;
using System.Collections.Generic;

namespace ArchivenewDomain.Model;

public partial class User: Entity
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Email { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public virtual SearchHistory UserNavigation { get; set; } = null!;

    public virtual ICollection<UserReference> UserReferences { get; set; } = new List<UserReference>();
}
