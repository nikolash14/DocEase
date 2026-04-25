namespace DocEase.Domain.Entities
{
    public record Patient
    {
        public int PatientId { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Gender { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public string? Phone { get; init; }
        public string? Email { get; init; }
        public DateTime? RegistrationDate { get; init; }
        public int? UserId { get; init; }
    }
}
