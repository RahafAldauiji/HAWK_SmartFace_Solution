using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartfaceSolution.Entities;
using SmartfaceSolution.SubEntities;
using SmartfaceSolution.Email;
namespace SmartfaceSolution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine(new SubMatchFaces().matchFaces().ToString());
            // new SubWatchlist().search();
            //  "C://Users//rahaf//Downloads//bb820d72-a6d8-4900-9952-fc74c0256d72");
            Email.Email.sendEmail();
            CreateHostBuilder(args).Build().Run(); 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}