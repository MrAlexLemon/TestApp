using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebService.Entities;
using WebService.Interfaces;
using WebService.Persistence;

namespace WebService.Hubs
{
    public class ServiceHub : Hub<IMessage>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ServiceHub(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task SendDataToAzure(ComputerInfo computerInfo)
        {
            Console.WriteLine("Get data from client.");
            //Send data to Azure

            /* HttpClient _client = new HttpClient();
             HttpRequestMessage newRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:7071/api/ComputerInfoRequest");
             HttpResponseMessage response = await _client.SendAsync(newRequest);

             if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
             {
                 //throw new Exception("Process Failed by Azure function.");
                 Console.WriteLine("Something went wrong.");
             }
             else
                 Console.WriteLine("Data was processed.");*/

            var computer = await _applicationDbContext.ComputerInfos.FirstOrDefaultAsync(x => x.ComputerName == computerInfo.ComputerName);

            if (computer == null)
            {
                await _applicationDbContext.ComputerInfos.AddAsync(new ComputerInfo()
                {
                    ComputerName = computerInfo.ComputerName,
                    DotNetVersion = computerInfo.DotNetVersion,
                    OsName = computerInfo.OsName,
                    TimeZone = computerInfo.TimeZone,
                    ConnectedTime = DateTime.UtcNow
                });
            }
            else
            {
                computer.ComputerName = computerInfo.ComputerName;
                computer.DotNetVersion = computerInfo.DotNetVersion;
                computer.OsName = computerInfo.OsName;
                computer.TimeZone = computerInfo.TimeZone;
                _applicationDbContext.ComputerInfos.Update(computer);
            }
            await _applicationDbContext.SaveChangesAsync();
        }

        public override async Task OnConnectedAsync()
        {
            //await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} joined to hub.");

            //Update status and date in db to online

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} left hub.");

            //Update status and date in db to offline

            var context = this.Context.GetHttpContext();
            string compName = context.Request.Headers["ComputerName"];

            var computer = await _applicationDbContext.ComputerInfos.FirstOrDefaultAsync(x => x.ComputerName == compName);

            computer.DisconnectedTime = DateTime.UtcNow;

            _applicationDbContext.ComputerInfos.Update(computer);
            await _applicationDbContext.SaveChangesAsync();

            await base.OnDisconnectedAsync(exception);
        }
    }
}
