using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ChallengeApp
{
    public static class DreidelSpin
    {    
        [FunctionName("DreidelSpin")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {   
            log.LogInformation("request received.");
            
            return new OkObjectResult(new Spinner().Spin());                
        }
    }
}
