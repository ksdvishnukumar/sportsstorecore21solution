using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace SportsStoreCore21WebApp.Controllers
{
  public class AboutController: Controller
  {
    public IActionResult Index() => View();

    public IActionResult Throw()
    {
      throw new EntryPointNotFoundException("This is a user thrown exception");
    }
  }
}
