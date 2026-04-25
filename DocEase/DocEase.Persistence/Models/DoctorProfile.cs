using System;
using System.Collections.Generic;

namespace DocEase.Persistence.Models;

public partial class DoctorProfile
{
    public int ProfileId { get; set; }

    public int? DoctorId { get; set; }

    public string? Specialization { get; set; }

    public string? Qualifications { get; set; }

    public int? ExperienceYears { get; set; }

    public string? Bio { get; set; }

    public virtual Doctor? Doctor { get; set; }
}
