using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AppInsightsHelperFunctions
{

  public static class SampleHttpFunction
  {
    [FunctionName("SampleHttpFunction")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log, ExecutionContext context)
    {
      log.LogInformation("SampleHttpFunction processed a request.");

      return new OkObjectResult("OK");
    }

    
  }

}

