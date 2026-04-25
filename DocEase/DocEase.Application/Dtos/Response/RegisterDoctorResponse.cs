namespace DocEase.Application.Dtos.Response
{
    public record RegisterDoctorResponse
    {
        public required string Name {  get; init; }
        public int DoctorId { get; init; }
        public required string Gender { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public required string Phone { get; init; }
        public required string Email { get; init; }
        public DateTime? RegistrationDate { get; init; }
        public int? UserId { get; init; }
    }
}
