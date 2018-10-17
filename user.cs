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
using System.Collections.Generic;
using System.Threading;
using System.Security;
using System.Web;
using System.Collections.Specialized;
using Microsoft.Extensions.Primitives;
using System.Net.Http;
using System.Net.Http.Headers;

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
            // IDictionary<string, StringValues> d = req.Headers;
            // JObject pJOtClaims = new JObject();
            // foreach (KeyValuePair<string, StringValues> pair in d){
            //     log.LogInformation("{0}:{1}",pair.Key,(string)pair.Value);
            //     pJOtClaims.Add(pair.Key, new JValue((string)pair.Value));
            // }
            string accessToken = req.Headers["x-ms-token-aad-access-token"];
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://graph.microsoft.com/v1.0/me");
            var cont = await response.Content.ReadAsStringAsync();
    //         var me = JsonConvert.DeserializeObject(cont);

    //         CloudUserInfo currentUser = new CloudUserInfo();
    //         currentUser.upn = me.UserPrincipalName;
    //         ViewData["UserPrincipalName"] = 
    // ViewData["DisplayName"] = me.DisplayName;
    // ViewData["Mail"] = me.Mail;
    // ViewData["me"] = cont;
 
    // return View();

            return (ActionResult)new OkObjectResult(JsonConvert.DeserializeObject(cont));
            // Thread t = Thread.CurrentThread;
            // HttpContext.
            // if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
            // {
            //     log.LogInformation("Not authenticated");
            //     return (ActionResult)new OkObjectResult("An0n? is it you?");  
            // }

            // ClaimsIdentity identity = (Thread.CurrentPrincipal as ClaimsPrincipal)?.Identity as ClaimsIdentity;
            // foreach (var claim in identity.Claims)
            // {
            // log.LogInformation($"{claim.Type} = {claim.Value}");
            // }

            // try
            // {
            //     JObject pJOtClaims = new JObject();
            // ClaimsPrincipal me = ClaimsPrincipal.Current;
            // IEnumerable<System.Security.Claims.ClaimsIdentity> mes = ClaimsPrincipal.Current.Identities;
            // foreach(Claim curClaim in  ClaimsPrincipal.Current.Claims)
            // //ClaimsPrincipal.Current.Identities.First().Claims)
            // {
            //     // if (Array.IndexOf(fields,curClaim.Type) >= 0)
            //     // {   
            //     //     string key = curClaim.Type.Substring(curClaim.Type.LastIndexOf("/") +1);
            //     //     pJOtClaims.Add(key, new JValue(curClaim.Value));
            //     // }
            //     log.LogInformation(curClaim.Type + " : " + curClaim.Value);
            // }
            //     return (ActionResult)new OkObjectResult(pJOtClaims);
            // }
            // catch (System.Exception)
            // {
            //     log.LogError()
            //     return (ActionResult)new OkObjectResult("An0n? is it you?");            
            // }
            
        }
    }
}
