# Dataverse Monitoring resources with Application Insights

# Introduction 
Collaboration project for WE BizApps CSAs to work on providing useful queries and workbooks for Application Insights monitoring Dataverse resources based on the standard [Dataverse to Application Insights](https://learn.microsoft.com/en-us/power-platform/admin/analyze-telemetry) integration.

# Contents
This repository contains **queries** in KQL format and **Azure workbooks** in JSON format.

# Instructions - Workbooks
To create an Azure Workbook based on this sample:
1. Download the [workbook JSON source](workbooks\DataverseMonitoringWorkbook101.json)
2. In a text editor, replace the string **APPINSIGHTSRESOURCEID** with the target Application Insights resource. Example: */subscriptions/mySubscriptionId/resourceGroups/myResourceGroup/providers/microsoft.insights/components/myAppInsightsResource*
3. Navigate to your Azure portal and then to your Application Insights resource.
4. Click on Workbooks - New
5. Click on Advanced Editor
6. Paste the code from the text editor in step 2.