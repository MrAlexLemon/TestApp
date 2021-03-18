using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Common.Models;
using ProcessComputerInfoFunction.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ProcessComputerInfoFunction
{
    public class Function1
    {
        private readonly ApplicationDbContext todoContext;

        public Function1(ApplicationDbContext todoContext)
        {
            this.todoContext = todoContext;
        }



        [FunctionName("ComputerInfoRequest")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ComputerInfo data = JsonConvert.DeserializeObject<ComputerInfo>(requestBody);

            var currentComputer1 = await todoContext.ComputerInfos.FirstOrDefaultAsync(x => x.ComputerName == data.ComputerName);

            var connectionstring = "Persist Security Info=False;User ID=sa;Password=password123Admin789;Database=computers;Server=sqlserver1;";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);


            using (ApplicationDbContext dbContext = new ApplicationDbContext(optionsBuilder.Options))
            {
                var currentComputer = await dbContext.ComputerInfos.FirstOrDefaultAsync(x => x.ComputerName == data.ComputerName);

                if (currentComputer == null)
                    return new NotFoundResult();

                currentComputer.ComputerStatus.Date = DateTime.UtcNow;
                currentComputer.DotNetVersion = data.DotNetVersion;
                currentComputer.OsName = data.OsName;
                currentComputer.TimeZone = data.TimeZone;
                currentComputer.ComputerStatus.IsOnline = data.ComputerStatus.IsOnline;

                try
                {
                    dbContext.ComputerInfos.Update(currentComputer);
                    await dbContext.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    return new BadRequestResult();
                }
            }

            string responseMessage = "This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
