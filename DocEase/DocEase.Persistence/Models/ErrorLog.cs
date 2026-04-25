using System;
using System.Collections.Generic;

namespace DocEase.Persistence.Models;

public partial class ErrorLog
{
    public int Id { get; set; }

    public string? Message { get; set; }

    public DateTime TimeStamp { get; set; }

    public string? Exception { get; set; }

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public string? UserId { get; set; }
}
