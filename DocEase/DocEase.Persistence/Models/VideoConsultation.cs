using System;
using System.Collections.Generic;

namespace DocEase.Persistence.Models;

public partial class VideoConsultation
{
    public int ConsultationId { get; set; }

    public int AppointmentId { get; set; }

    public string MeetingLink { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Status { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
}
