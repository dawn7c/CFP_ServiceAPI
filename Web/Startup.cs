using Domain.Abstractions;
using Infrastructure.DatabaseContext;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Swashbuckle.AspNetCore.SwaggerUI;
using Infrastructure.LoggerConfig;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            CustomLogger.ConfigureLogger();
            services.AddLogging(builder =>
            {
                builder.AddSerilog();
            });
            services.AddControllers();
            
            services.AddScoped(typeof(IRepository<>), typeof(BidRepository<>));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CFP_Service", Version = "1.0" });
            });
            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(connectionString));

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI();

        }
    }
}
