using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProductsCategories.Models;

public class CategoryController : Controller
{
  private ProductCategoryContext _context;

  public CategoryController(ProductCategoryContext context)
  {
    _context = context;
  }

  [HttpGet("categories")]
  public IActionResult ShowCategories()
  {
    List<Category> ListOfCategories = _context.Categories.ToList();
    ViewBag.ListOfCategories = ListOfCategories;
    return View("Category");
  }

  [HttpPost("categories/create")]
  public IActionResult CreateCategory(Category newCategory)
  {
    if (ModelState.IsValid)
    {
      _context.Categories.Add(newCategory);
      _context.SaveChanges();
      return RedirectToAction("ShowCategories");
    }
    return ShowCategories();
  }

  [HttpGet("categories/{categoryId}")]
  public IActionResult ShowOneCategory(int categoryId)
  {
    Category? foundCategory = _context.Categories.FirstOrDefault(category => category.CategoryId == categoryId);

    Category? myCategory = _context.Categories.Include(c => c.AssociatedProducts).ThenInclude(assocProd => assocProd.Product).First(product => product.CategoryId == categoryId);

    if (foundCategory != null)
    {
      ViewBag.CategoryName = foundCategory.Name;
      ViewBag.CurrentCategoryId = categoryId;
      List<Product> AllExistingProducts = _context.Products.ToList();
      ViewBag.AllExistingProducts = AllExistingProducts;

      // var AllProductsInThisCategory = _context.Products.Include(c => c.AssociatedCategories).ThenInclude(p => p.Product).ToList();
      ViewBag.myCategory = myCategory;

      return View("SingleCategory");
    }
    return ShowCategories();
  }

  [HttpPost("association/category")]
  public IActionResult AssociateProductToCategory(Association newAssociation)
  {
    Association? associationID = _context.Associations.FirstOrDefault(assoc => assoc.CategoryId == newAssociation.CategoryId);
    if (ModelState.IsValid)
    {
      // if (associationID == null)
      // {
      // }
      // ViewBag.AssociationError = "This category already contains this product!";
      // return ShowCategories();
      _context.Associations.Add(newAssociation);
      _context.SaveChanges();
      return RedirectToAction("ShowCategories", "Category");
    }
    return View("Category");
  }
}