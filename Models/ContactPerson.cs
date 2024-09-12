using System;
using System.Collections.Generic;

namespace CRMV2.Models;

public partial class ContactPerson
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int? ContractId { get; set; }

    public virtual Contract? Contract { get; set; }
}
