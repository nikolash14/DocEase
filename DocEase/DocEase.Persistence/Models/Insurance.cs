using System;
using System.Collections.Generic;

namespace DocEase.Persistence.Models;

public partial class Insurance
{
    public int InsuranceId { get; set; }

    public int PatientId { get; set; }

    public string Provider { get; set; } = null!;

    public string PolicyNumber { get; set; } = null!;

    public string? CoverageDetails { get; set; }

    public DateOnly ValidTill { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
