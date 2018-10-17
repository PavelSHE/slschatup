using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Linq;

namespace Arc.Function
{
    public static class user
    {
        //toDo add claims verification thru graph API
        private static string[] fields = {"ipaddr","name","http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname","http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"};

        [FunctionName("user")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            JObject pJOtClaims = new JObject();
            foreach(Claim curClaim in  ClaimsPrincipal.Current.Identities.First().Claims)
            {
                if (Array.IndexOf(fields,curClaim.Type) >= 0)
                {   
                    string key = curClaim.Type.Substring(curClaim.Type.LastIndexOf("/") +1);
                    pJOtClaims.Add(key, new JValue(curClaim.Value));
                }
                log.LogInformation(curClaim.Type + " : " + curClaim.Value);
            }
            return (ActionResult)new OkObjectResult(pJOtClaims);
        }
    }
}
