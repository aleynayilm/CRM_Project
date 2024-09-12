using System;
using System.Collections.Generic;

namespace CRMV2.Models;

public partial class Contract
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string? Name { get; set; }

    public DateTime? BeginDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? Active { get; set; }

    public int? Type { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<ContactPerson> ContactPeople { get; set; } = new List<ContactPerson>();

    public virtual ContactsType? TypeNavigation { get; set; }
}
