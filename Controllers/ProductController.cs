using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProductsCategories.Models;

public class ProductController : Controller
{
  private ProductCategoryContext _context;

  public ProductController(ProductCategoryContext context)
  {
    _context = context;
  }

  [HttpGet("products")]
  public IActionResult ShowProducts()
  {
    return View("ShowProducts");
  }
}