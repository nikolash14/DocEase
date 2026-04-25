using System;
using System.Collections.Generic;

namespace DocEase.Persistence.Models;

public partial class PatientDiseaseHistory
{
    public int HistoryId { get; set; }

    public int? PatientId { get; set; }

    public string? DiseaseName { get; set; }

    public DateOnly? DiagnosisDate { get; set; }

    public string? Notes { get; set; }

    public virtual Patient? Patient { get; set; }
}
