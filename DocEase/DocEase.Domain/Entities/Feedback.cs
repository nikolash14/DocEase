namespace DocEase.Domain.Entities
{
    public record Feedback
    {
        public int FeedbackId { get; init; }
        public int? PatientId { get; init; }
        public int? DoctorId { get; init; }
        public int? Rating { get; init; }
        public string? Comments { get; init; }
        public DateTime? FeedbackDate { get; init; }
    }
}
