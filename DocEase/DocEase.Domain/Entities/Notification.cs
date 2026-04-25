namespace DocEase.Domain.Entities
{
    public record Notification
    {
        public int NotificationId { get; init; }
        public string? UserType { get; init; }
        public int? UserId { get; init; }
        public string? Message { get; init; }
        public DateTime? CreatedDate { get; init; }
        public bool? IsRead { get; init; }
    }
}
