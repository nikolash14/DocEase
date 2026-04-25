namespace DocEase.Domain.Entities
{
    public record Payment
    {
        public int PaymentId { get; init; }
        public int PatientId { get; init; }
        public decimal Amount { get; init; }
        public DateTime? PaymentDate { get; init; }
        public string? Method { get; init; }
        public string? Status { get; init; }
        public string? UTR { get; init; }
    }
}
