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
using SmartfaceSolution.Extensions;
using SmartfaceSolution.MatchScop;
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
            
           // services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
           services.AddScoped<IMatchService, MatchService>();
           services.AddHostedService<BackBroundS>();
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
            //services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()));
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
            //app.ConfigureExceptionHandler(env);
            app.UseMiddleware<ExceptionMiddleware>();
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }else
            // {
            //     app.UseExceptionHandler("/Error");
            //     app.UseHsts();
            // }

           // app.UseMiddleware<ExceptionMiddlewareExtensions>();
           
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            //
            app.UseAuthentication();
            app.UseMiddleware<JwtMiddleware>();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            //
            // var webSocketOptions = new WebSocketOptions() 
            // {
            //     KeepAliveInterval = TimeSpan.FromSeconds(120),
            // };
            
            
            //
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

    }
}