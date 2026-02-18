using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;

namespace AppInsightsHelperFunctions
{
  public class TimerSendFormData
  {
    [FunctionName("TimerSendFormData")]
    async public Task Run([TimerTrigger("0 0 */24 * * *")] TimerInfo myTimer, ILogger log, ExecutionContext context)
    {
      log.LogInformation($"TimerSendFormData executed at: {DateTime.Now}");

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


      string formDataQuery = "systemforms()?$select=name,objecttypecode";

      HttpRequestMessage getFormDataRequest = new()
      {
        RequestUri = new Uri($"{config["DataverseAPI.Url"]}/api/data/v{config["DataverseAPI.Version"]}/{formDataQuery}")
      };

      getFormDataRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);


      HttpResponseMessage getFormDataResponse = await client.SendAsync(getFormDataRequest);
      JObject formsJson = JObject.Parse(await getFormDataResponse.Content.ReadAsStringAsync());
      JArray formInformationRecords = (JArray)formsJson.GetValue("value");

      log.LogInformation($"Found {formInformationRecords.Count} forms, sending telemetry...");

      foreach (JObject formInformationRecord in formInformationRecords)
      {
        string formId = (string)formInformationRecord.GetValue("formid");
        string formName = (string)formInformationRecord.GetValue("name");
        string objectTypeCode = (string)formInformationRecord.GetValue("objecttypecode");
        var formData = new Dictionary<string, string>() { { "formId", formId }, { "formName", formName }, { "table", objectTypeCode } };
        appInsights.TrackEvent("formInformation", formData);
      }

      log.LogInformation("Flushing...");
      appInsights.Flush();
    }


  }
}
