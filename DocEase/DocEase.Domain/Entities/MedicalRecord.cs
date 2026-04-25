namespace DocEase.Domain.Entities
{
    public record MedicalRecord
    {
        public int RecordId { get; init; }
        public int? PatientId { get; init; }
        public int? DoctorId { get; init; }
        public string? RecordType { get; init; }
        public string? Description { get; init; }
        public DateTime? CreatedDate { get; init; }
    }
}
