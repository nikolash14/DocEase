using System;
using System.Collections.Generic;

namespace DocEase.Persistence.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
