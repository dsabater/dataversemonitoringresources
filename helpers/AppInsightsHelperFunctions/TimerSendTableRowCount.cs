using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AppInsightsHelperFunctions
{
  public class TimerSendTableRowCount
  {
    [FunctionName("TimerSendTableRowCount")]
    public void Run([TimerTrigger("0 0 */24 * * *")] TimerInfo myTimer, ILogger log, ExecutionContext context)
    {
      log.LogInformation($"-- TimerSendTableRowCount executed at: {DateTime.Now}");

      var config = new ConfigurationBuilder()
      .SetBasePath(context.FunctionAppDirectory)
      .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
      .AddEnvironmentVariables()
      .Build();

      string tablesString = "account,contact,solutioncomponent,activitypointer,mailbox,import";

      string[] tables = tablesString.Split(',');

      log.LogInformation($"Getting row counts for '{tablesString}'");
      
      string tdsDataverseConnectionString = config["tdsDataverseConnectionString"];
      string appInsightsConnectionString = config["appInsightsConnectionString"];

      TelemetryClient appInsights = new(new TelemetryConfiguration { ConnectionString = appInsightsConnectionString });

      using SqlConnection tds = new(tdsDataverseConnectionString);
      tds.Open();

      foreach (var table in tables)
      {
        SqlCommand sqlCommand = new() { Connection = tds, CommandText = $"SELECT COUNT(*) FROM {table}" };
        int numberOfRecords = (int)sqlCommand.ExecuteScalar();
        appInsights.TrackMetric($"rowcount {table}", (double)numberOfRecords);

        log.LogInformation($"{table}: {numberOfRecords:#####}");
      }
      appInsights.Flush();
    }
  }
}
