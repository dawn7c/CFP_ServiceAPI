using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
