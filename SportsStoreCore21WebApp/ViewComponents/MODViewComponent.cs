using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SportsStoreCore21WebApp.ViewComponents
{
  public class MODViewComponent : ViewComponent
  {
    private readonly IConfiguration _configuration;

    public MODViewComponent(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public Task<IViewComponentResult> InvokeAsync()
    {
      var result = _configuration["MOD"];
      return Task.FromResult<IViewComponentResult>(View("Default", result));
    }
  }
}
