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

namespace Challenge
{
    public static class GitHubWebhook
    {
        [FunctionName("GitHubWebhook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [Table("ImagesTable")] IAsyncCollector<GiftImage> tableBinding,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<SimplePayload>(requestBody);
            
            foreach (var commit in data.Commits)
            {
                foreach (var file in commit.Added)
                {
                    if (file.EndsWith(".png"))
                    {
                       await tableBinding.AddAsync(new GiftImage 
                                                    {
                                                        Id = Guid.NewGuid(), 
                                                        ImageUrl = $"{data.Repository.Url}/raw/master/{file}"
                                                    });
                    }                    
                }                
            }            
            
            return new OkObjectResult("OK");
                
        }
    }

    public class SimplePayload
    {
        public List<Commit> Commits { get; set; }
        public Repository Repository { get; set; }
        

    }
    public  class Repository
    {
        public string Url { get; set; }
    }
    public class Commit 
    {
        public string Id { get; set; }
        public string[] Added { get; set; }

    }
    
    public class GiftImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }

    }

}
