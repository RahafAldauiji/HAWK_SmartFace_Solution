using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebApi.Helpers;
using WebApi.Services;

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
           // services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.AddControllers();
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IUserService, UserService>();
            // var key = Encoding.ASCII.GetBytes(Settings.Secret);
            // services.AddAuthentication(x =>
            //     {
            //         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     })
            //     .AddJwtBearer(x =>
            //     {
            //         x.RequireHttpsMetadata = false;
            //         x.SaveToken = true;
            //         x.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(key),
            //             ValidateIssuer = false,
            //             ValidateAudience = false
            //         };
            //
            //     });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

             app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            //
            app.UseAuthentication();
            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //         name: "default",
            //         template: "{controller}/{action}/");
            // });
            app.UseMiddleware<JwtMiddleware>();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

    }
}