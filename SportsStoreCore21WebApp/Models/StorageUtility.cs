using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;

namespace SportsStoreCore21WebApp.Models
{
  public class StorageUtility
  {
    public string StorageAccountName { get; set; }

    public string StorageAccountAccessKey { get; set; }

    public CloudStorageAccount StorageAccount
    {
      get {
        string account = StorageAccountName;
        if (account == "{StorageAccountName}")
        {
          // Local on-prem storage
          return CloudStorageAccount.DevelopmentStorageAccount;
        }
        else
        {
          // Azure Cloud Storage
          string key = StorageAccountAccessKey;
          string connectionString = $"DefaultEndpointsProtocol=https;AccountName={account};AccountKey={key}";
          return CloudStorageAccount.Parse(connectionString);
        }
      }
    }
  }
}
