using DocEase.Application.Config;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Data;

namespace DocEase.Api.Middleware
{
    public static class SerilogConfiguration
    {
        public static LoggingLevelSwitch LevelSwitch = new LoggingLevelSwitch
        {
            MinimumLevel = LogEventLevel.Warning
        };

        public static void ConfigureLogger(
            HostBuilderContext context,
            IServiceProvider provider,
            LoggerConfiguration logger)
        {
            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new List<SqlColumn>
                {
                    new() { ColumnName = "Controller", DataType = SqlDbType.NVarChar, DataLength = 100 },
                    new() { ColumnName = "Action", DataType = SqlDbType.NVarChar, DataLength = 100 },
                    new() { ColumnName = "UserId", DataType = SqlDbType.NVarChar, DataLength = 100 }
                },
                Store = new List<StandardColumn> {
                    StandardColumn.Message,
                    StandardColumn.TimeStamp,
                    StandardColumn.Exception
                }
            };

            columnOptions.TimeStamp.ConvertToUtc = true;
            var serilogSetting = provider.GetRequiredService<IOptions<SerilogSetting>>().Value;
            if (serilogSetting is not null)
            {
                logger
                    .MinimumLevel.ControlledBy(LevelSwitch)
                    .Enrich.FromLogContext()
                    // File sink
                    .WriteTo.File(
                        path: $"{serilogSetting.LogFileFolder}/{serilogSetting.LogFileName}.json",
                        rollingInterval: RollingInterval.Hour,
                        outputTemplate: serilogSetting?
                                            .LogFileOutputTemplate ??
                                            "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}",
                        retainedFileTimeLimit: TimeSpan.FromDays(
                                            serilogSetting?
                                            .LogFileRetainedFileTimeLimitinDays ?? 10)
                    )
                    // SQL Server sink
                    .WriteTo.MSSqlServer(
                        connectionString: provider.GetRequiredService<IOptions<SqlSetting>>().Value.SqlServerPrimary,
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = serilogSetting?.ErrorTable ?? "ErrorLogs",
                            SchemaName = serilogSetting?.ErrorTableSchema ?? "dbo",
                            AutoCreateSqlTable = serilogSetting?.AutoCreateSqlTable ?? false
                        },
                        columnOptions: columnOptions
                    );
            }
        }

    }
}
