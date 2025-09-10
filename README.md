# Dataverse Monitoring resources with Application Insights

# Introduction 
Collaboration project for WE BizApps CSAs to work on providing useful queries and workbooks for Application Insights monitoring Dataverse resources based on the standard [Dataverse to Application Insights](https://learn.microsoft.com/en-us/power-platform/admin/analyze-telemetry) integration.

# Contents
This repository contains **queries** in KQL format and **Azure workbooks** in JSON format.

# Queries
- [dataverse-active-users-timeline.kql](queries/dataverse-active-users-timeline.kql)  
  *Active users by time – shows unique users per hour as a column chart.*

- [dataverse-api-pass-rate.kql](queries/dataverse-api-pass-rate.kql)  
  *API Pass rate – calculates the % of time API requests were successful.*

- [dataverse-api-throttle-details.kql](queries/dataverse-api-throttle-details.kql)  
  *Returns details of requests that have been throttled due to Dataverse protection limits (HTTP 429).*

- [dataverse-api-throttle-timeline.kql](queries/dataverse-api-throttle-timeline.kql)  
  *Returns requests that have been throttled due to Dataverse protection limits as a timechart.*

- [dataverse-exception-details.kql](queries/dataverse-exception-details.kql)  
  *Returns detalils on exceptions including entityName and problemId, with links for workbooks.*

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




# Instructions - Workbooks
To create an Azure Workbook based on this sample:
1. Download the [workbook JSON source](workbooks/DataverseMonitoringWorkbook101.json)
2. In a text editor, replace the string **APPINSIGHTSRESOURCEID** with the target Application Insights resource. Example: */subscriptions/mySubscriptionId/resourceGroups/myResourceGroup/providers/microsoft.insights/components/myAppInsightsResource*
3. Navigate to your Azure portal and then to your Application Insights resource.
4. Click on Workbooks - New
5. Click on Advanced Editor
6. Paste the code from the text editor in step 2.


