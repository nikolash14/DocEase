namespace DocEase.Application.Dtos.Request
{
    public record DoctorFilterRequest(
        string Name,
        string Email,
        int YearOfExperience,
        string Department,
        string Specialization,
        string State,
        string City,
        string Area
        );
}
