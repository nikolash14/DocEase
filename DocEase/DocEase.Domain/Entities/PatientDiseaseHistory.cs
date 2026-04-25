namespace DocEase.Domain.Entities
{
    public record PatientDiseaseHistory
    {
        public int HistoryId { get; init; }
        public int? PatientId { get; init; }
        public string? DiseaseName { get; init; }
        public DateTime? DiagnosisDate { get; init; }
        public string? Notes { get; init; }
    }
}
