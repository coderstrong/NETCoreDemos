using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog.Settings.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.File;
using Serilog.Sinks.Async;
using Serilog.Sinks.Email;
using System.Net;

namespace ApiRedisCache
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        public static void Main(string[] args)
        {
            // var emailInfo = new EmailConnectionInfo(){
            //     EmailSubject = "Serilog Mail",
            //     EnableSsl = false,
            //     FromEmail = "xxxxx@gmail.com",
            //     ToEmail ="xxxxx@gmail.com",
            //     NetworkCredentials = new NetworkCredential {
            //         UserName = "xxxx@gmail.com",
            //         Password = "xxxx",
            //     },
            //     MailServer ="smtp.googlemail.com"
            // };

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            // .WriteTo.Email(emailInfo)
            .CreateLogger();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
