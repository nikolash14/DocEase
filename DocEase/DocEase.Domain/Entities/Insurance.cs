namespace DocEase.Domain.Entities
{
    public record Insurance
    {
        public int InsuranceId { get; init; }
        public int PatientId { get; init; }
        public string? Provider { get; init; }
        public string? PolicyNumber { get; init; }
        public string? CoverageDetails { get; init; }
        public DateTime ValidTill { get; init; }
    }
}
