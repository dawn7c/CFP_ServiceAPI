using Serilog;
using Serilog.Events;

namespace Infrastructure.LoggerConfig
{
    public class CustomLogger
    {
        public static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.File("D:\\andrey loh (projects)\\CFP_Service\\logs\\log.txt", rollingInterval: RollingInterval.Infinite)
                .CreateLogger();
        }
    }
}
