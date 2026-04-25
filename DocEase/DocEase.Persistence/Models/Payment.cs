using System;
using System.Collections.Generic;

namespace DocEase.Persistence.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int PatientId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string Method { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Utr { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
