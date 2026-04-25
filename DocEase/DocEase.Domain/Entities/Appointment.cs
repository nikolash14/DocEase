namespace DocEase.Domain.Entities
{
    public record Appointment
    {
        public int AppointmentId { get; init; }
        public int? PatientId { get; init; }
        public int? DoctorId { get; init; }
        public int? ScheduleId { get; init; }
        public DateTime? AppointmentDate { get; init; }
        public string? Status { get; init; }
    }
}
