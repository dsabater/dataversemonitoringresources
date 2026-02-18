# Dataverse Monitoring resources with Application Insights

# Introduction 
Collaboration project made by BizApps CSAs who work on providing useful queries and workbooks for monitoring [Dataverse](#dataverse-queries) and [Power Automate](#power-automate-queries) with Application Insights, based on the standard [Dataverse to Application Insights](https://learn.microsoft.com/en-us/power-platform/admin/analyze-telemetry) integration.

# Contents
This repository contains **queries** in KQL format and **Azure workbooks** in JSON format.


# AppInsights Helper Functions

Example C# project with helper functions for Azure Application Insights in the context of Dataverse telemetry. This project provides utility methods to simplify the integration of custom metrics that are not automatically captured by Dataverse telemetry, like user information, table row count, and other custom data points. It is based on **Azure Functions** with the overall idea to be run periodically to capture custom metrics and send them to Azure Application Insights for monitoring and analysis.

**Table row count**
This example captures the row count of a set of tables in Dataverse and publishes it as a _customMetric_. This is particularly useful for monitoring the growth of data and identifying potential performance issues.

**User information**
This example captures user information, such as systemuserid and email address, and publishes it as a _customEvent_. This can help in understanding user engagement and identifying trends in user activity.

**Form data**
This example captures additional Dataverse Form data, such as form name, and publishes it as a _customEvent_. This can help in monitoring the performance of forms and identifying potential bottlenecks.

**View data**
This example captures additional Dataverse View data, such as view name, and publishes it as a _customEvent_. This can help in monitoring the performance of views and identifying potential bottlenecks.





# Queries


## Dataverse queries

- [dataverse-active-users-timeline.kql](queries/dataverse-active-users-timeline.kql)  
  *Active users by time – shows unique users per hour as a column chart.*

- [dataverse-api-pass-rate.kql](queries/dataverse-api-pass-rate.kql)  
  *API Pass rate – calculates the % of time API requests were successful.*

- [dataverse-api-throttle-details.kql](queries/dataverse-api-throttle-details.kql)  
  *Returns details of requests that have been throttled due to Dataverse protection limits (HTTP 429).*

- [dataverse-api-throttle-timeline.kql](queries/dataverse-api-throttle-timeline.kql)  
  *Returns requests that have been throttled due to Dataverse protection limits as a timechart.*

- [dataverse-exception-details.kql](queries/dataverse-exception-details.kql)  
  *Returns details on exceptions including entityName and problemId, with links for workbooks.*

- [dataverse-exceptions-by-entity.kql](queries/dataverse-exceptions-by-entity.kql)  
  *Returns exception count by Entity (table) and ProblemId.*

- [dataverse-exceptions-timeline.kql](queries/dataverse-exceptions-timeline.kql)  
  *Returns a time series of exception count.*

- [dataverse-one-view-performance.kql](queries/dataverse-one-view-performance.kql)  
  *Performance for one view – shows average duration and hits per hour for a specific view.*

- [dataverse-operations-cud.kql](queries/dataverse-operations-cud.kql)  
  *Summarizes create, update, upsert, and delete operations by table.*

- [dataverse-operations-formloads.kql](queries/dataverse-operations-formloads.kql)  
  *Summarizes EditForm loads by table, load type, and boot status.*

- [dataverse-operations-timeline.kql](queries/dataverse-operations-timeline.kql)  
  *Operations by time – requests per hour by action type.*

- [dataverse-ops-actions-timeline.kql](queries/dataverse-ops-actions-timeline.kql)  
  *Returns actions as timeline, with average and 95th percentile duration per hour.*

- [dataverse-ops-actions.kql](queries/dataverse-ops-actions.kql)  
  *Returns operations against different tables, with execution count and duration.*

- [dataverse-ops-page-loads-timeline.kql](queries/dataverse-ops-page-loads-timeline.kql)  
  *Returns page loads for timeline visualization, with average and 95th percentile duration per hour.*

- [dataverse-ops-page-loads.kql](queries/dataverse-ops-page-loads.kql)  
  *Return page loads, with load type mapping for workbook visualization.*

- [dataverse-ops-users-timeline.kql](queries/dataverse-ops-users-timeline.kql)  
  *Returns requests by user type for a timeline visualization.*

- [dataverse-pageviews-by-hour-of-day.kql](queries/dataverse-pageviews-by-hour-of-day.kql)  
  *Shows page views by hour of day and page name as a column chart.*

- [dataverse-plugin-exec-avgduration-time.kql](queries/dataverse-plugin-exec-avgduration-time.kql)  
  *Returns a time series for average plugin execution duration (timechart).*

- [dataverse-plugin-exec-count-error-time.kql](queries/dataverse-plugin-exec-count-error-time.kql)  
  *Returns time series for total and failed plugin executions (timechart).*

- [dataverse-plugin-exec-overview.kql](queries/dataverse-plugin-exec-overview.kql)  
  *Summarizes plugin executions: error count, total count, and average duration by name.*

- [dataverse-plugin-exec-s-vs-f.kql](queries/dataverse-plugin-exec-s-vs-f.kql)  
  *Returns plugin execution count split by success/failure (for pie chart).*

- [dataverse-plugin-exec-summary.kql](queries/dataverse-plugin-exec-summary.kql)  
  *Returns error count, total count, and average duration for plugins (for tile visualization).*

- [dataverse-query-details.kql](queries/dataverse-query-details.kql)  
  *Shows the top 10 slowest queries for a given table.*

- [dataverse-query-duration-timeline.kql](queries/dataverse-query-duration-timeline.kql)  
  *Returns a time series of average query duration (timechart).*

- [dataverse-query-target-duration.kql](queries/dataverse-query-target-duration.kql)
  *Returns SDKRetrieveMultiple queries summarized by avg duration and table*

- [dataverse-search-exceptions.kql](queries/dataverse-search-exceptions.kql)  
  *Searches all fields of the exceptions table for a given search term.*

- [dataverse-top10-form-loads.kql](queries/dataverse-top10-form-loads.kql)  
  *Shows the top 10 most common form loads by table, with average load time.*

- [dataverse-top10-view-loads.kql](queries/dataverse-top10-view-loads.kql)  
  *Shows the top 20 most common view loads by table and view, with average load time.*

- [dataverse-uci-loads-by-country-barchart.kql](queries/dataverse-uci-loads-by-country-barchart.kql)  
  *Groups different load times by country for barchart visualization.*

- [dataverse-uci-loads-by-table.kql](queries/dataverse-uci-loads-by-table.kql)  
  *Summarizes page loads using PageViews and workbook parameters, grouped by table.*

- [dataverse-uci-loads-by-user-loadcounts.kql](queries/dataverse-uci-loads-by-user-loadcounts.kql)  
  *Summarizes load counts by user for use in barchart, including cold and warm loads.*

- [dataverse-uci-loads-by-user-loadtimes.kql](queries/dataverse-uci-loads-by-user-loadtimes.kql)  
  *Summarizes load times by user for use in barchart, showing average cold and warm load times.*

- [dataverse-uci-loads-by-user-table.kql](queries/dataverse-uci-loads-by-user-table.kql)  
  *Groups the loads and timing by user and table, including cold and warm load counts.*


## Power Automate queries

- [powerautomate-flowerrors.kql](queries/powerautomate-flowerrors.kql)  
  *Shows failed Cloud Flow actions with error details and task names.*

- [powerautomate-flownames.kql](queries/powerautomate-flownames.kql)  
  *Lists distinct Cloud Flow display names and IDs.*

- [powerautomate-flowrunsbysuccess.kql](queries/powerautomate-flowrunsbysuccess.kql)  
  *Summarizes Cloud Flow runs by flow name and success status.*

- [powerautomate-flowsbybillableactions.kql](queries/powerautomate-flowsbybillableactions.kql)  
  *Shows top 10 Cloud Flows by billable actions.*

- [powerautomate-flowsbyruns.kql](queries/powerautomate-flowsbyruns.kql)  
  *Shows top 10 Cloud Flows by number of runs.*

- [powerautomate-navigatetoflowrun.kql](queries/powerautomate-navigatetoflowrun.kql)  
  *Builds direct URLs to failed Cloud Flow runs for investigation.*

- [powerautomate-triggererrors.kql](queries/powerautomate-triggererrors.kql)  
  *Summarizes failed Cloud Flow triggers by flow name.*


# Instructions - Workbooks
To create an Azure Workbook based on this sample:
1. Download the [workbook JSON source](workbooks/DataverseMonitoringWorkbook101.json)
2. In a text editor, replace the string **APPINSIGHTSRESOURCEID** with the target Application Insights resource. Example: */subscriptions/mySubscriptionId/resourceGroups/myResourceGroup/providers/microsoft.insights/components/myAppInsightsResource*
3. Navigate to your Azure portal and then to your Application Insights resource.
4. Click on Workbooks - New
5. Click on Advanced Editor
6. Paste the code from the text editor in step 2.


