using System;
using System.Collections.Generic;

namespace DocEase.Persistence.Models;

public partial class DoctorSchedule
{
    public int ScheduleId { get; set; }

    public int? DoctorId { get; set; }

    public DateOnly? AvailableDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public bool? IsBooked { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Doctor? Doctor { get; set; }
}
