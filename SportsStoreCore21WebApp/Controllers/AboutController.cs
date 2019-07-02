using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SportsStoreCore21WebApp.Models;

namespace SportsStoreCore21WebApp.Controllers
{
  public class AboutController: Controller
  {
    private readonly IOptions<StorageUtility> _storageUtility;
    private readonly IConfiguration _configuration;

    public AboutController(IOptions<StorageUtility> storageUtility, IConfiguration configuration)
    {
      _storageUtility = storageUtility;
      _configuration = configuration;
    }

    public IActionResult Index()
    {
      //var dbPassword = _configuration["DbPassword"];
      //var result = $"The Value of DbPassword in KeyVault is '{dbPassword}'";
      //return View("Index", result);


      var result = _storageUtility.Value;
      var aboutDetails = new Dictionary<string, string>()
      {
        { "StorageAccountInformation--StorageAccountName", result.StorageAccountName},
        { "StorageAccountInformation--StorageAccountAccessKey", result.StorageAccountAccessKey},
        { "SportsStoreStorageConnection", _configuration["SportsStoreStorageConnection"]},
        { "ConnectionStrings--SportsStoreDbConnection", _configuration["ConnectionStrings:SportsStoreDbConnection"] },
        //{ "ConnectionStrings--RedisConnection", _configuration["ConnectionStrings:RedisConnection"] },
        //{ "ApplicationInsights--InstrumentationKey", _configuration["ApplicationInsights:InstrumentationKey"] }
      };
      return View("Index", aboutDetails);
    }

    public async Task<IActionResult> VaultResults()
    {
      AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
      KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
      var secrets = await keyVaultClient.GetSecretsAsync($"{_configuration["SportsStoreKeyVault"]}");//.ConfigureAwait(false);
      //return View(secrets);
      Dictionary<string, string> secretValueList = new Dictionary<string, string>();
      foreach (var item in secrets)
      {
        var secret = await keyVaultClient.GetSecretAsync($"{item.Id}");
        secretValueList.Add(item.Id, secret.Value);
      }
      return View(secretValueList);
    }

    public IActionResult Throw()
    {
      throw new EntryPointNotFoundException("This is a user thrown exception");
    }
  }
}
