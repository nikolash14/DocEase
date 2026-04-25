namespace DocEase.Domain.Entities
{
    public record Prescription
    {
        public int PrescriptionId { get; init; }
        public int? PatientId { get; init; }
        public int? DoctorId { get; init; }
        public string? Medication { get; init; }
        public string? Dosage { get; init; }
        public string? Duration { get; init; }
        public string? Notes { get; init; }
        public DateTime? DateIssued { get; init; }
    }
}
