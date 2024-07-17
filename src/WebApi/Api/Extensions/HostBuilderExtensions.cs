using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.SystemConsole.Themes;

namespace Papirus.WebApi.Api.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder AddSerilog(this IHostBuilder builder, IConfiguration configuration)
    {
        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
            .WriteTo.File(new CompactJsonFormatter(), "Logs/log.json", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7);

        var appInsightConnectionString = configuration["ApplicationInsights:ConnectionString"];
        loggerConfig.WriteTo.ApplicationInsights(
            appInsightConnectionString,
            TelemetryConverter.Traces);

        var applicationDbContextConnectionString = configuration["ConnectionStrings:DefaultConnection"];
        if (!string.IsNullOrWhiteSpace(applicationDbContextConnectionString))
        {
            loggerConfig.WriteTo.MSSqlServer(
                connectionString: applicationDbContextConnectionString,
                sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = false });
        }

        Serilog.Log.Logger = loggerConfig.CreateLogger();

        builder.UseSerilog();

        return builder;
    }
}