using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

//custom 
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace Arc.Function
{
    public static class tester
    {
        private static string configurationVariable(string name)
            => System.Environment.GetEnvironmentVariable(name);

        private static string firstNonEmpty(string[] arr){
            return arr.FirstOrDefault(s => !string.IsNullOrEmpty(s)) ?? "";
        }

        [FunctionName("tester")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Tester triggered");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);


            string file = firstNonEmpty(new string[]{req.Query["file"],data?.file,appconfig.defaultFile});
            string name = firstNonEmpty(new string[]{req.Query["name"],data?.name,"An0nym0us"});

            Dictionary<string,string> common = new Dictionary<string, string>();
            common.Add("name",name);
            common.Add("file",file);
            common.Add("basePath",appconfig.basePath);

            string json = JsonConvert.SerializeObject(common, Formatting.Indented);

            return (ActionResult)new OkObjectResult(json);
        }
    }
}
