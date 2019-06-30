using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsStoreCore21WebApp.Models.Abstract;
using SportsStoreCore21WebApp.Models.Entities;

namespace SportsStoreCore21WebApp.Controllers
{
  public class ProductController : Controller
  {
    private readonly IConfiguration _configuration;
    private readonly IProductRepository _productRepository;
    private readonly IPhotoService _photoService;

    public ProductController(IProductRepository productRepository, IPhotoService photoService, IConfiguration configuration)
    {
      _productRepository = productRepository;
      _photoService = photoService;
      _configuration = configuration;
    }
    public IActionResult Index() => View();
    public async Task<IActionResult> List()
    {
      var productsList = await _productRepository.GetAllProductsAsync();
      return View(productsList);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind(include: "ProductName, Description, Price, Category, PhotoUrl")] Product product, IFormFile photo)
    {
      if (ModelState.IsValid)
      {
        product.PhotoUrl = await _photoService.UploadPhotoAsync(product.Category, photo);
        await _productRepository.CreateAsync(product);
        TempData["newproduct"] = $"New Product: '{product.ProductName}' with Id: '{product.ProductId}' has been added successfully";
        return RedirectToAction("List");
      }
      return View(product);
    }

    public async Task<IActionResult> Edit(int productId)
    {
      var result = await _productRepository.FindProductByIdAsync(productId);
      return View(result);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Product product, IFormFile photo)
    {
      if (photo == null) { }
      else
      {
        if (await _photoService.DeletePhotoAsync(product.Category, product.PhotoUrl))
        {
          product.PhotoUrl = await _photoService.UploadPhotoAsync(product.Category, photo);
        }
      }
      await _productRepository.UpdateAsync(product);
      return RedirectToAction("List");
    }

    public async Task<IActionResult> GetByCategory(string category)
    {
      ViewBag.category = category;

      var result = await _productRepository.FindProductsByCategoryAsync(category);
      return View(result);
    }


    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int productId)
    {
      var result = await _productRepository.FindProductByIdAsync(productId);
      if (await _photoService.DeletePhotoAsync(result.Category, result.PhotoUrl))
      {
        await _productRepository.DeleteAsync(productId);
      }
      return RedirectToAction("List");
    }


  }
}
