namespace DocEase.Application.Dtos.Request
{
    public record RegisterDoctorRequest
        (
        string FirstName,
        string LastName,
        int Gender,
        string PhoneNumber,
        string Email,
        DateTime DateOfBirth
        );
}
