// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace QuickstartIdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ThreadPool.GetMinThreads(out var workThreads, out var completionPortThreads);
            ThreadPool.SetMinThreads(workThreads, workThreads * 125);

            //Change SSL checks so that all checks pass
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
               new RemoteCertificateValidationCallback(
                    delegate
                    { return true; }
                );

            Console.Title = "IdentityServer";

            var seed = args.Contains("/seed");
            if (seed)
            {
                args = args.Except(new[] { "/seed" }).ToArray();
            }

            var host = BuildWebHost(args);

            //if (seed)
            //{
                SeedData.EnsureSeedData(host.Services);
            //}

            host.Run();
        }

        /// <summary>
        /// BuildWebHost
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHost BuildWebHost(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)

            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(options =>//设置Kestrel服务器
                {
                    options.ListenLocalhost(44320, listenOptions =>
                    {
                        listenOptions.UseHttps(new X509Certificate2("idx2.pfx", "xuwei"));
                    });
                })
            .ConfigureAppConfiguration(c =>
            {
                // config.AddApollo(hostingconfig.GetSection("apollo"))
                //.AddNamespace("Net1.Test");
                //c.AddDefault();
            });
                webBuilder.UseStartup<Startup>();
            })
                //.UseStartup<Startup>()
                //.UseKestrel(options =>
                //{
                //    // Set properties and call methods on options
                //})
                .Build();

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //    .UseKestrel(options =>//设置Kestrel服务器
        //    {
        //        options.ListenLocalhost(44320, listenOptions =>
        //        {
        //            listenOptions.UseHttps("ids.pfx", "xuwei");
        //        });
        //        //options.Listen(IPAddress.Parse("172.16.0.10"), 5002, listenOptions =>
        //        //{
        //        //    //填入之前iis中生成的pfx文件路径和指定的密码　　　　　　　　　　　　
        //        //    listenOptions.UseHttps(pfx, "123456");
        //        //});
        //    })
        //    .ConfigureAppConfiguration(c => {
        //        // config.AddApollo(hostingconfig.GetSection("apollo"))
        //        //.AddNamespace("Net1.Test");
        //        //c.AddDefault();
        //    })
        //        .UseStartup<Startup>()
        //        .Build();
    }
}