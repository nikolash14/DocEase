namespace DocEase.Domain.Entities
{
    public record DoctorProfile
    {
        public int ProfileId { get; init; }
        public int? DoctorId { get; init; }
        public string? Specialization { get; init; }
        public string? Qualifications { get; init; }
        public int? ExperienceYears { get; init; }
        public string? Bio { get; init; }
    }
}
