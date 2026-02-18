using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Microsoft.PowerPlatform.Dataverse.Client;
using System;

namespace AppInsightsHelperFunctions
{
  public class TimerSendUserInformation
  {
    [FunctionName("TimerSendUserInformation")]
    public void Run([TimerTrigger("0 * */24 * * *")] TimerInfo myTimer, ILogger log, ExecutionContext context)
    {
      log.LogTrace($"TimerSendUserInformation executed at: {DateTime.Now}");  

      // Get the configuration from local.settings.json or Azure App Configuration
      var config = new ConfigurationBuilder()
     .SetBasePath(context.FunctionAppDirectory)
     .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
     .AddEnvironmentVariables()
     .Build();

      // Get the AppInsights connection string from the configuration
      string appInsightsConnectionString = config["appInsightsConnectionString"];

      // Create a new AppInsights client
      TelemetryClient appInsights = new(new TelemetryConfiguration { ConnectionString = appInsightsConnectionString });

      // Get the Dataverse connection string from the configuration
      string dataverseConfigurationString = config["dataverseConnectionString"];

      // Get all active users from Dataverse
      var dv = new ServiceClient(dataverseConfigurationString);
      QueryByAttribute query = new QueryByAttribute("systemuser") { 
        ColumnSet = new ColumnSet("systemuserid", "fullname", "address1_city", "azureactivedirectoryobjectid") };
      query.AddAttributeValue("isdisabled", false);

      EntityCollection ret = dv.RetrieveMultiple(query);

      // Go through all returned users
      foreach (var user in ret.Entities)
      {
        // Create a dictionary with the user information
        var userData = new Dictionary<string, string>() {
          { "crmUserId", user.Id.ToString() },
          { "fullname", user.GetAttributeValue<string>("fullname") },
          { "aadUserId", user.GetAttributeValue<Guid>("azureactivedirectoryobjectid").ToString() },
          { "city",  user.GetAttributeValue<string>("address1_city") }
        };
        // Send the user information to AppInsights
        appInsights.TrackEvent("userInformation", userData);
      }
      // Flush the data to AppInsights
      appInsights.Flush();

      log.LogTrace($"TimerSendUserInformation completed at: {DateTime.Now}");
    }
  }
}
