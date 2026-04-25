namespace DocEase.Domain.Entities
{
    public record ErrorLog
    {
        public int Id { get; init; }
        public string? Message { get; init; }
        public DateTime TimeStamp { get; init; }
        public string? Exception { get; init; }
        public string? Controller { get; init; }
        public string? Action { get; init; }
        public string? UserId { get; init; }
    }

}
