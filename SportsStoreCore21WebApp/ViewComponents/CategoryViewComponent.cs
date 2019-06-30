using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SportsStoreCore21WebApp.Models;
using SportsStoreCore21WebApp.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStoreCore21WebApp.ViewComponents
{
  public class CategoryViewComponent : ViewComponent
  {
    private readonly IOptions<StorageUtility> _storageUtility;

    public CategoryViewComponent(IOptions<StorageUtility> storageUtility)
    {
      _storageUtility = storageUtility;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      CloudStorageAccount cloudStorageAccount = _storageUtility.Value.StorageAccount;

      CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

      var currentToken = new BlobContinuationToken();
      List<string> categoriesContainerList = new List<string>();
      var result = await cloudBlobClient.ListContainersSegmentedAsync(currentToken);
      if (result.Results.Count() != 0)
      {
        categoriesContainerList = result.Results.Select(c =>c.Name).ToList();
      }
      return await Task.FromResult<IViewComponentResult>(View("Default", categoriesContainerList));
    }

  }
}
