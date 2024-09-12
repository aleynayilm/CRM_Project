using System;
using System.Collections.Generic;

namespace CRMV2.Models;

public partial class Log
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string? Thread { get; set; }

    public string? Level { get; set; }

    public string? Logger { get; set; }

    public string? Message { get; set; }

    public int? UserId { get; set; }
    
    public virtual User User { get; set; }
}
