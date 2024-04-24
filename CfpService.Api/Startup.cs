using CfpService.Domain.Abstractions;
using CfpService.DataAccess.ActivityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CfpService.Application.Convertors;
using CfpService.DataAccess.ApplicationRepository;
using CfpService.DataAccess.DatabaseContext;

namespace CfpService.Api
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
            
            services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(new ActivityConverter());
               });
            
            services.AddScoped(typeof(IApplication), typeof(ApplicationRepository));
            services.AddScoped(typeof(IActivity), typeof(ActivityRepository));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CFP_Service", Version = "1.0" });
            });

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
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
