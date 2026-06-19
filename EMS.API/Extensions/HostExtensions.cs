using Serilog;

namespace EMS.API.Extensions
{
    public static class HostExtensions
    {
        public static ConfigureHostBuilder AddLogging(this ConfigureHostBuilder host)
        {
            host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId());
            return host;
        }
    }
}