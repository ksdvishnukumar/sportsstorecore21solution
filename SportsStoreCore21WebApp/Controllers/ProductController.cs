using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace SportsStoreCore21WebApp.Controllers
{
  public class ProductController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
    public IActionResult List()
    {
      return View();
    }
  }
}
