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
    List<Category> AllExistingCategories = _context.Categories.ToList();
    List<Association> AllCategoriesOfProduct = _context.Associations.ToList();
    ViewBag.AllCategoriesOfProduct = AllCategoriesOfProduct;
    ViewBag.AllExistingCategories = AllExistingCategories;
    if (foundProduct == null)
    {
      ViewBag.ProductError = "A product with that id was not found!";
      return ShowProducts();
    }
    else
    {
      ViewBag.ProductName = foundProduct.Name;
      ViewBag.CurrentProductId = foundProduct.ProductId;
    }

    return View("SingleProduct");
  }

  [HttpPost("association/product")]
  public IActionResult AssociateCategoryToProduct(Association newAssociation)
  {
    Association? associationID = _context.Associations.FirstOrDefault(assoc => assoc.ProductId == newAssociation.ProductId);
    if (ModelState.IsValid)
    {
      if (associationID == null)
      {
        _context.Associations.Add(newAssociation);
        _context.SaveChanges();
        return RedirectToAction("ShowProducts", "Product");
      }
      ViewBag.AssociationError = "This product already exists in this category!";
      return ShowProducts();
    }
    return View("Products");
  }

}