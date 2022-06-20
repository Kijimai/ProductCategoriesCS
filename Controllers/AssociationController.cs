using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProductsCategories.Models;

public class AssociationController : Controller
{
  private ProductCategoryContext _context;

  public AssociationController(ProductCategoryContext context)
  {
    _context = context;
  }

  [HttpPost("association/category")]
  public IActionResult AssociateProductToCategory(Association newAssociation)
  {
    if (ModelState.IsValid)
    {
      _context.Associations.Add(newAssociation);
      _context.SaveChanges();
      return RedirectToAction("ShowCategories", "Category");
    }
    return View("Category");
  }

  public IActionResult AssociateCategoryToProduct(Association newAssociation)
  {
    if (ModelState.IsValid)
    {
      _context.Associations.Add(newAssociation);
      _context.SaveChanges();
      return RedirectToAction("ShowProducts", "Product");
    }
    return View("Products");
  }
}