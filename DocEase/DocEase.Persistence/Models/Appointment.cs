using System;
using System.Collections.Generic;

namespace DocEase.Persistence.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public int? ScheduleId { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public string? Status { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual DoctorSchedule? Schedule { get; set; }

    public virtual ICollection<VideoConsultation> VideoConsultations { get; set; } = new List<VideoConsultation>();
}
