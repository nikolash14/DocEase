using System;
using System.Collections.Generic;

namespace DocEase.Persistence.Models;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public string? Medication { get; set; }

    public string? Dosage { get; set; }

    public string? Duration { get; set; }

    public string? Notes { get; set; }

    public DateTime? DateIssued { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
