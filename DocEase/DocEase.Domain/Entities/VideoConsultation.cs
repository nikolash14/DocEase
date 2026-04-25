namespace DocEase.Domain.Entities
{
    public record VideoConsultation
    {
        public int ConsultationId { get; init; }
        public int AppointmentId { get; init; }
        public string? MeetingLink { get; init; }
        public DateTime StartTime { get; init; }
        public DateTime EndTime { get; init; }
        public string? Status { get; init; }
    }
}
