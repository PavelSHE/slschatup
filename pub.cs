using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Arc.Function
{
    public static class pub
    {
        [FunctionName("pub")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string relativePath = HelpFunctions.FirstNonEmpty(new string[]{req.Query["file"],appconfig.defaultFile});
            log.LogInformation("Requested : " + relativePath);

            string serverFilePath = Path.Combine(appconfig.basePath,relativePath);
            log.LogInformation("Requested : " + serverFilePath);

            try{
                return new FileContentResult(HelpFunctions.GetFile(serverFilePath), HelpFunctions.GetMimeType(serverFilePath));
            }
            catch{
                return new BadRequestObjectResult("File not found");
            }
            
        }
    }
}
