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
    List<Product> CurrentProducts = _context.Products.ToList();
    ViewBag.CurrentProducts = CurrentProducts;
    return View("Products");
  }

  [HttpPost("/products/create")]
  public IActionResult CreateProduct(Product newProduct)
  {
    if (ModelState.IsValid)
    {
      _context.Products.Add(newProduct);
      _context.SaveChanges();
      return RedirectToAction("ShowProducts");
    }
    return ShowProducts();
  }

  [HttpGet("products/{productId}")]
  public IActionResult ShowOneProduct(int productId)
  {
    Product? foundProduct = _context.Products.FirstOrDefault(product => product.ProductId == productId);
    if (foundProduct == null)
    {
      ViewBag.ProductError = "A product with that id was not found!";
      return ShowProducts();
    }

    return View("SingleProduct");
  }
}