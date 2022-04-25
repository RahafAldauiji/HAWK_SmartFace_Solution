using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartfaceSolution.Entities;
using SmartfaceSolution.Helpers;
using SmartfaceSolution.SubEntities;
using SmartfaceSolution.Message;
using SmartfaceSolution.Models;

namespace SmartfaceSolution
{
    
    public class Program
    {
       
        public static async Task Main(string[] args)
        {
            //Console.WriteLine(new SubMatchFaces().matchFaces().ToString());
            // new SubWatchlist().search();
            //  "C://Users//rahaf//Downloads//bb820d72-a6d8-4900-9952-fc74c0256d72");
           // Message.Message m = new Message.Message(2, "Rahaf", "17/12/2021");
           // m.sendEmail("rahafaldauiji12@gmail.com");
           // SubEntities.SubMatchFaces.matchSocket();
           //Console.WriteLine(await new SubWatchlistMember().retrievesImage("31578ad1-81c8-44a1-8e42-2e080771908c".Trim()));
           
           CreateHostBuilder(args).Build().Run(); 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}