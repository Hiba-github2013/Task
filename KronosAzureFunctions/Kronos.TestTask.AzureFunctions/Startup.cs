using Kronos.TestTask.businessLayer;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: FunctionsStartup (typeof(Kronos.TestTask.AzureFunctions.Startup))]
namespace Kronos.TestTask.AzureFunctions
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IGamesAzureFunction, PostGames>();
         }
    }
}
