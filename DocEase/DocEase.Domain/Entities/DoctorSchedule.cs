namespace DocEase.Domain.Entities
{
    public record DoctorSchedule
    {
        public int ScheduleId { get; init; }
        public int? DoctorId { get; init; }
        public DateTime? AvailableDate { get; init; }
        public TimeSpan? StartTime { get; init; }
        public TimeSpan? EndTime { get; init; }
        public bool? IsBooked { get; init; }
    }
}
