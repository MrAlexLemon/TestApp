using ConsoleApp.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ComputerInfo computerInfo = new ComputerInfo
            {
                ComputerName = Environment.MachineName.ToString(),
                TimeZone = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).ToString(),
                OsName = System.Environment.OSVersion.Platform.ToString(),
                DotNetVersion = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName,
            };

            var connection = new HubConnectionBuilder()
                .WithUrl(HubHelper.HubUrl, options =>
                {
                    options.Headers["ComputerName"] = Environment.MachineName.ToString();
                })
                .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
                .Build();

            connection.On(HubHelper.Events.PingEvent, async () =>
            {
                Console.WriteLine("Pong!");
                await connection.InvokeCoreAsync("SendDataToAzure", args: new[] { computerInfo });
            });


            while (true)
            {
                try
                {
                    await connection.StartAsync();

                    break;
                }
                catch
                {
                    await Task.Delay(1000);
                }
            }

            Console.WriteLine("Client One listening. Hit Ctrl-C to quit.");
            Console.ReadLine();
        }
    }
}
