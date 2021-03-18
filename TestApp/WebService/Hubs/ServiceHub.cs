using Common.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebService.Hubs
{
    public class ServiceHub : Hub<IMessage>
    {
        public async Task SendDataToAzure(ComputerInfo computerInfo)
        {
            Console.WriteLine("Get data from client.");
            //Send data to Azure

            HttpClient _client = new HttpClient();
            HttpRequestMessage newRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:7071/api/ComputerInfoRequest");
            HttpResponseMessage response = await _client.SendAsync(newRequest);

            if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
            {
                //throw new Exception("Process Failed by Azure function.");
                Console.WriteLine("Something went wrong.");
            }
            else
                Console.WriteLine("Data was processed.");
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

            await base.OnDisconnectedAsync(exception);
        }
    }
}
