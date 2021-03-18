using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProcessComputerInfoFunction.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(ProcessComputerInfoFunction.Startup))]
namespace ProcessComputerInfoFunction
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //string connectionString = "Persist Security Info=False;User ID=sa;Password=password123Admin789;Database=computers;Server=sqlserver1;";
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, Environment.GetEnvironmentVariable("SqlConnectionString")));
        }
    }
}
