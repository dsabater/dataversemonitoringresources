using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AppInsightsHelperFunctions
{
  public class Util
  {
    public static async Task<string> GetAccessToken(IPublicClientApplication app, IConfiguration config)
    {
      List<string> scopes = new() { $"{config["DataverseAPI.Url"]}/user_impersonation" };

      var accounts = await app.GetAccountsAsync();

      AuthenticationResult? result;
      if (accounts.Any())
        result = await app.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync();
      else
      {
        // These samples use username/password for simplicity, but it is not a recommended pattern.
        // More information: 
        //https://docs.microsoft.com/azure/active-directory/develop/scenario-desktop-acquire-token?tabs=dotnet#username-and-password

        if (string.IsNullOrWhiteSpace((string)config["DataverseAPI.Password"]) || string.IsNullOrWhiteSpace((string)config["DataverseAPI.UserPrincipalName"]))
          throw new Exception("User name or Password not found in settings.");

        try
        {
          string password = (string)config["DataverseAPI.Password"];

          result = await app.AcquireTokenByUsernamePassword(scopes.ToArray(), (string)config["DataverseAPI.UserPrincipalName"], password)
              .ExecuteAsync();
        }
        catch (MsalUiRequiredException)
        {
          // You will get the following error if your UserPrincipalName or Password in appsettings.config is incorrect:
          /*
            Microsoft.Identity.Client.MsalClientException
            HResult=0x80131500
            Message=Only loopback redirect uri is supported, but app://58145b91-0c36-4500-8554-080854f2ac97/ was found. Configure http://localhost or http://localhost:port both during app registration and when you create the PublicClientApplication object. See https://aka.ms/msal-net-os-browser for details
            Source=Microsoft.Identity.Client
          */

          //Open browser to enter credentials when MFA required
          result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

        }

      }

      if (result != null && !string.IsNullOrWhiteSpace(result.AccessToken))
        return result.AccessToken;
      else
        throw new Exception("Failed to get access token.");
    }
  }
}
