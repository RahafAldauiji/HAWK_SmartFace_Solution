using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartfaceSolution.Helpers;
using SmartfaceSolution.Services;
using SmartfaceSolution.Middleware;

namespace SmartfaceSolution
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AnotherPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            services.Configure<ServerConfig>(Configuration.GetSection("ConnectionStrings")); // inject the ConnectionStrings 
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig")); // inject the JwtConfig 
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<ISearchDB, SearchDB>();
            services.AddHostedService<BackgroundMatchService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            app.UseRouting();
            app.UseCors(); 
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}