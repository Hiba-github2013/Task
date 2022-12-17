using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kronos.TestTask.AzureFunctions;
using Kronos.TestTask.Core;
using Kronos.TestTask.businessLayer;
public class GameDataExtractor
{
    private readonly IGamesAzureFunction GamesService;
    public GameDataExtractor(IGamesAzureFunction GamesService)
    {
        this.GamesService = GamesService;
    }

    [FunctionName("GameDataExtractor")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        Game games = JsonConvert.DeserializeObject<Game>(requestBody);
        List<DataToDisplay> DataList = GamesService.PostGamesList(games.Results);
        string responseMessage = "";

        if (DataList.Count > 0) { responseMessage = "OK"; }

        log.LogInformation("C# HTTP trigger function processed a request." + responseMessage);
        return new OkObjectResult(DataList);
    }
}
