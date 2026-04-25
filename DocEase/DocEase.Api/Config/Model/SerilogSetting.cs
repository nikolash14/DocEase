namespace DocEase.Api.Config.Model
{
    public class SerilogSetting
    {
        public string? ErrorTable {  get; set; }
        public string? ErrorTableSchema { get; set; }
        public Boolean AutoCreateSqlTable { get; set; }
        public string? LogFileName { get; set; }
        public string? LogFileFolder { get; set; }
        public string? LogFileRollingInterval { get; set; }
        public string? LogFileOutputTemplate { get; set; }
        public int LogFileRetainedFileTimeLimitinDays { get; set; }
    }
}
