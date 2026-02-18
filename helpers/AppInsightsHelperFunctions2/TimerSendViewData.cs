using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace AppInsightsHelperFunctions
{
    public class TimerSendViewData
    {
        [FunctionName("TimerSendViewData")]
        async public Task Run([TimerTrigger("0 0 */24 * * *")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
      log.LogInformation($"TimerSendViewData executed at: {DateTime.Now}");

      var config = new ConfigurationBuilder()
        .SetBasePath(context.FunctionAppDirectory)
        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();


      IPublicClientApplication app = PublicClientApplicationBuilder.Create(config["DataverseAPI.ClientId"])
            .WithRedirectUri(config["DataverseAPI.RedirectUri"])
            .WithAuthority(config["DataverseAPI.Authority"])
            .Build();


      string token = await Util.GetAccessToken(app, config);
      log.LogInformation($"Token acquired {token.Substring(0, 10)}...");

      string appInsightsConnectionString = config["appInsightsConnectionString"];
      TelemetryClient appInsights = new(new TelemetryConfiguration { ConnectionString = appInsightsConnectionString });

      HttpClient client = new();

      string viewDataQuery = "savedqueries()?$select=iscustom,isdefault,isquickfindquery,name,isuserdefined,querytype,returnedtypecode,savedqueryid&$filter=componentstate eq 0 and statecode eq 0";


      HttpRequestMessage getViewDataRequest = new()
      {
        RequestUri = new Uri($"{config["DataverseAPI.Url"]}/api/data/v{config["DataverseAPI.Version"]}/{viewDataQuery}")
      };

      getViewDataRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

      HttpResponseMessage getViewDataResponse = await client.SendAsync(getViewDataRequest);
      string viewsDataString = await getViewDataResponse.Content.ReadAsStringAsync();
      JObject viewsJson = JObject.Parse(viewsDataString);
      JArray viewInformationRecords = (JArray)viewsJson.GetValue("value");

      log.LogInformation($"Found {viewInformationRecords.Count} views, sending telemetry...");

      foreach (JObject viewInfoRecord in viewInformationRecords)
      {
        string viewId = (string)viewInfoRecord.GetValue("savedqueryid");
        string viewName = (string)viewInfoRecord.GetValue("name");
        string returnedTypeCode = (string)viewInfoRecord.GetValue("returnedtypecode");
        var viewData = new Dictionary<string, string>() { { "viewId", viewId }, { "viewName", viewName }, { "table", returnedTypeCode } };
        appInsights.TrackEvent("viewInformation", viewData);
      };

      log.LogInformation("Flushing...");
      appInsights.Flush();

    }
  }
}
