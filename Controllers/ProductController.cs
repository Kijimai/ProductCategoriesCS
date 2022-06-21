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

    Product? myProduct = _context.Products.Include(product => product.AssociatedCategories).ThenInclude(assocCateg => assocCateg.Category).First(product => product.ProductId == productId);

    ViewBag.myProduct = myProduct;
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
    Association? associationItem = _context.Associations.First(assoc => assoc.ProductId == newAssociation.ProductId);
    if (ModelState.IsValid)
    {
      if (associationItem == null)
      {
        _context.Associations.Add(newAssociation);
        _context.SaveChanges();
        return RedirectToAction("ShowProducts", "Product");
      }
      ViewBag.Debug = associationItem;
      ViewBag.AssociationError = "This product already exists in this category!";
      return ShowProducts();
    }
    return View("Products");
  }

}