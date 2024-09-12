using System;
using System.Collections.Generic;

namespace CRMV2.Models;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Adress { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
