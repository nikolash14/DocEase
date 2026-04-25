using DocEase.Api.Config.Model;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Data;

namespace DocEase.Api.Middleware
{
    public static class SerilogConfiguration
    {

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
            logger
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                // File sink
                .WriteTo.File(
                    path: $"{serilogSetting.LogFileFolder}/{serilogSetting.LogFileName}.json",
                    rollingInterval: RollingInterval.Hour,
                    outputTemplate: serilogSetting.LogFileOutputTemplate,
                    retainedFileTimeLimit: TimeSpan.FromDays(serilogSetting.LogFileRetainedFileTimeLimitinDays)
                )
                // SQL Server sink
                .WriteTo.MSSqlServer(
                    connectionString: provider.GetRequiredService<IOptions<SqlSetting>>().Value.SqlServerPrimary,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = serilogSetting.ErrorTable,
                        SchemaName = serilogSetting.ErrorTableSchema,
                        AutoCreateSqlTable = serilogSetting.AutoCreateSqlTable
                    },
                    columnOptions: columnOptions
                );
        }

    }
}
