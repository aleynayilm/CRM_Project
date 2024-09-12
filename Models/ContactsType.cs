using System;
using System.Collections.Generic;

namespace CRMV2.Models;

public partial class ContactsType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
